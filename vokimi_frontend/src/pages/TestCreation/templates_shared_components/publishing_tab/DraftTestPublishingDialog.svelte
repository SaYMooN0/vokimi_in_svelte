<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import EditingDialogCloseButton from "../../creation_shared_components/editing_dialog_components/EditingDialogCloseButton.svelte";
    import TestPublishedSuccessfully from "./TestPublishedSuccessfully.svelte";

    export let testId: string;
    let dialogElement: BaseDialog;
    let publishingProblems: { category: string; message: string }[] = [];
    let errorMessage: string = "";
    let testIsPublishedInfo: {
        isPublished: boolean;
        publishedTestId: string;
        publishedTestName: string;
    } = {
        isPublished: false,
        publishedTestId: "",
        publishedTestName: "",
    };

    export async function open() {
        errorMessage = "";
        await fetchPublishingErrors();
        await dialogElement.open();
    }
    async function fetchPublishingErrors() {
        if (publishingProblems.length > 0) {
            return;
        }
        const response = await fetch(
            "/api/testCreation/checkDraftTestForPublishingErrors/" + testId,
        );
        if (response.ok) {
            publishingProblems = await response.json();
            console.log(publishingProblems);
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage == "Unknown error";
        }
    }
    async function publishTest() {
        if (publishingProblems.length > 0) {
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
            const data = await response.json();
            testIsPublishedInfo = {
                isPublished: true,
                publishedTestId: data.testId,
                publishedTestName: data.testName,
            };
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage == "Unknown error";
        }
    }
</script>

<BaseDialog dialogId="publishing-dialog" bind:this={dialogElement}>
    <div class="dialog-content">
        {#if testIsPublishedInfo.isPublished}
            <TestPublishedSuccessfully
                publishedTestId={testIsPublishedInfo.publishedTestId}
                publishedTestName={testIsPublishedInfo.publishedTestName}
            />
        {:else}
            {#if publishingProblems.length > 0}
                <p class="test-has-problems">
                    Test cannot be published because of the following problems
                </p>
                <span class="fix-problems"
                    >Please fix them before publishing</span
                >
                <div class="errors-list">
                    <span class="problem-list-col-label">Category</span>
                    <span class="problem-list-col-label">Problem message</span>
                    {#each publishingProblems as p}
                        <span class="problem-category">
                            {p.category}
                        </span>
                        <span class="problem-message">
                            {p.message}
                        </span>
                    {/each}
                </div>
            {:else}
                <p class="no-problems-found">
                    No problems were found in the test
                </p>
                <p class="test-is-ready">Test is ready to be published</p>
            {/if}
            <p class="error-message">{errorMessage}</p>
            <button
                class="publish-btn"
                disabled={publishingProblems.length > 0}
                class:publishBtnDisabled={publishingProblems.length > 0}
                on:click={publishTest}
            >
                Publish
            </button>
            <EditingDialogCloseButton onClose={() => dialogElement.close()} />
        {/if}
    </div>
</BaseDialog>

<style>
    .dialog-content {
        padding: 8px 40px;
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .test-has-problems {
        font-size: 22px;
        color: var(--text);
        margin: 8px 12px;
        margin-top: 22px;
    }
    .fix-problems {
        font-size: 20px;
        color: var(--text-faded);
    }
    .errors-list {
        max-height: 600px;
        max-width: min(80vw, 1200px);
        padding: 8px 0px;
        margin: 8px 0;
        overflow-y: auto;
        display: grid;
        grid-template-columns: auto 1fr;
        grid-row-gap: 12px;
        grid-column-gap: 20px;
    }
    .problem-list-col-label {
        font-size: 22px;
        color: var(--text);
        font-weight: 600;
    }
    .problem-category {
        font-size: 18px;
        color: var(--text);
    }
    .problem-message {
        font-size: 18px;
        color: var(--text);
    }
    .no-problems-found {
        font-size: 22px;
        color: var(--text-faded);
        margin: 0;
        margin-top: 12px;
    }
    .test-is-ready {
        margin-top: 12px;
        margin-bottom: 48px;
        font-size: 32px;
        color: var(--text);
        font-weight: 600;
    }
    .error-message {
        font-size: 18px;
        color: var(--text-faded);
        margin: 8px 12px;
    }
    .publish-btn {
        margin: 0 auto;
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 8px 24px;
        font-size: 22px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .publish-btn:hover {
        background-color: var(--primary-hov);
    }
    .publish-btn:disabled {
        opacity: 0.8;
        background-color: var(--text-faded) !important;
        cursor: not-allowed;
    }
</style>
