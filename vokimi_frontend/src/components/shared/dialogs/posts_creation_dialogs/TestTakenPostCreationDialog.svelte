<script lang="ts">
    import type { TestTemplate } from "../../../../ts/enums/TestTemplate";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import AuthorizeView from "../../../AuthorizeView.svelte";
    import BaseDialog from "../../../BaseDialog.svelte";
    import CloseButton from "../../CloseButton.svelte";
    import LoadingMessage from "../../LoadingMessage.svelte";

    export let receivedResultId: string;
    export let testId: string;

    let dialogElement: BaseDialog;
    let dialogOpened = false;

    async function fetchTestTakenPostCreationData(): Promise<string | Err> {
        const response = await fetch(
            `/api/postsCreation/getTestTakenPostCreationData/${testId}/${receivedResultId}`,
        );
        if (response.ok) {
            return "Everything went fine!";
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
    }
    export function open() {
        dialogOpened = true;
        dialogElement.open();
    }
</script>

<BaseDialog dialogId="post-creation-dialog" bind:this={dialogElement}>
    <CloseButton onClose={() => dialogElement.close()} />
    <AuthorizeView>
        <div slot="loading">
            <LoadingMessage />
        </div>
        <div slot="authenticated">
            <div class="dialog-content">
                {#if dialogOpened}
                    {#await fetchTestTakenPostCreationData() then fetchingDataResult}
                        {#if fetchingDataResult instanceof Err}
                            <p class="err-message">
                                {fetchingDataResult.toString()}
                            </p>
                        {:else}
                            <p class="post-creation-div">
                                {fetchingDataResult}
                            </p>
                        {/if}
                    {/await}
                {/if}
            </div>
        </div>
        <div slot="unauthenticated" class="login-div">
            <div class="dialog-content">
                <p>Please log in to your account to continue</p>
                <a href="/auth">Login</a>
            </div>
        </div>
    </AuthorizeView>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 12px 20px;
    }
    .close-btn {
        background-color: var(--text-faded);
    }
</style>
