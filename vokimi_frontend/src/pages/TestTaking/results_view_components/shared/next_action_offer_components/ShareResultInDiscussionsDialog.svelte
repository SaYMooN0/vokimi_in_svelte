<script lang="ts">
    import AuthorizeView from "../../../../../components/AuthorizeView.svelte";
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import LoadingMessage from "../../../../../components/shared/LoadingMessage.svelte";
    import type { TestTemplate } from "../../../../../ts/enums/TestTemplate";
    import { Err } from "../../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";

    export let receivedResultId: string;

    let dialogElement: BaseDialog;
    let dialogOpened = false;

    async function fetchShareResultInDiscussionsData(): Promise<string | Err> {
        const response = await fetch(
            `/api/view-test/getShareResultInDiscussions/${receivedResultId}`,
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
                    {#await fetchShareResultInDiscussionsData() then fetchingDataResult}
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
