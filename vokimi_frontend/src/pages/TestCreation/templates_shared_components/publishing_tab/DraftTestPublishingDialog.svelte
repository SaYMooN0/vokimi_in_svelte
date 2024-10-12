<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import EditingDialogCloseButton from "../../creation_shared_components/editing_dialog_components/EditingDialogCloseButton.svelte";
    import { navigate } from "svelte-routing";

    export let testId: string;
    let dialogElement: BaseDialog;
    let publishingErrors: { category: string; message: string }[] = [];
    let errorMessage: string = "";

    export async function open() {
        errorMessage = "";
        await dialogElement.open();
    }
    async function fetchPublishingErrors() {
        if (publishingErrors.length > 0) {
            return;
        }
        const response = await fetch(
            "/api/testCreation/checkDraftTestForErrors/" + testId,
        );
        if (response.ok) {
            publishingErrors = await response.json();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage == "Unknown error";
        }
    }
    async function publishTest() {
        await fetchPublishingErrors();
        if (publishingErrors.length > 0) {
            return;
        }
        const response = await fetch(
            "/api/testCreation/publishTest/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
            },
        );
        if (response.ok) {
            navigate("/my-tests")
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage == "Unknown error";
        }
    }
</script>

<BaseDialog dialogId="publishing-dialog" bind:this={dialogElement}>
    <EditingDialogCloseButton onClose={() => dialogElement.close()} />
    <div class="dialog-content">
        {#if publishingErrors.length > 0}
            <p class="test-has-errors">
                Test cannot be published because of the following errors. Please
                fix them before publishing.
            </p>
            <div class="errors-list">
                {#each publishingErrors as error}
                    <span class="error-category">
                        {error.category}
                    </span>
                    <span class="error-message">
                        {error.message}
                    </span>
                {/each}
            </div>
        {:else}
            <p class="test-is-published">Test is ready to be published</p>
        {/if}
        <p class="error-message">{errorMessage}</p>
        <button
            class="publish-btn"
            class:publishBtnDisabled={publishingErrors.length > 0}
            on:click={publishTest}
        >
            Publish
        </button>
    </div>
</BaseDialog>

<style>
    .publish-btn {
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 8px 20px;
        font-size: 20px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .publish-btn:hover {
        background-color: var(--primary-hov);
    }
    .publishBtnDisabled {
        opacity: 0.8;
        background-color: var(--text-faded) !important;
        cursor: not-allowed;
    }
</style>
