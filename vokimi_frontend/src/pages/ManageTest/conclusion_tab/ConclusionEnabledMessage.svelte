<script lang="ts">
    import ActionConfirmationDialog from "../../../components/shared/dialogs/ActionConfirmationDialog.svelte";
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";

    export let testId: string;
    export let updateTabData: () => void;
    let confirmationDialog: ActionConfirmationDialog;
    function openConfirmationDialog() {
        confirmationDialog.open(
            deleteConclusion,
            "Are you sure you want to delete conclusion for this test?",
        );
    }
    async function deleteConclusion(): Promise<Err> {
        const response = await fetch(
            `/api/manageTest/conclusion/deleteConclusion/${testId}`,
            {
                method: "DELETE",
            },
        );
        if (response.ok) {
            updateTabData();
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong.");
        }
    }
</script>

<ActionConfirmationDialog
    confirmBtnText="Delete"
    dialogId="removeConclusionConfirmationDialog"
    bind:this={confirmationDialog}
/>

<div class="conclusion-enabled-message">
    <p>This test has conclusion</p>
    <button class="remove-conclusion-btn" on:click={openConfirmationDialog}>
        Remove conclusion
    </button>
</div>

<style>
    .conclusion-enabled-message {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
    .conclusion-enabled-message {
        font-size: 22px;
        font-weight: 500;
    }
    .remove-conclusion-btn {
        background-color: var(--red-del);
        color: var(--back-main);
        font-size: 18px;
        padding: 6px 12px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }
    .remove-conclusion-btn:hover {
        background-color: var(--red-del-hov);
    }
</style>
