<script lang="ts">
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { getAuthDataForced } from "../../../../../ts/stores/authStore";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "./UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let newBannerColor: string = "";
    let errorMessage: string = "";

    export let updateParentElement: (newVal: string) => void;
    export let currentBannerColor: string;
    export function open() {
        newBannerColor = "#ffaff3";
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewBannerColor() {
        if (StringUtils.isNullOrWhiteSpace(newBannerColor)) {
            return;
        }
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateBannerColor", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newBannerColor),
        });

        if (response.ok) {
            await getAuthDataForced();
            updateParentElement(newBannerColor);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="aboutMeEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <h1 class="dialog-title">
            Customize your page banner:
            <h1>
                {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
                    <p class="error-message">{errorMessage}</p>
                {/if}
                <UpdateDialogSaveBtn onSaveClicked={saveNewBannerColor} />
            </h1>
        </h1>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        position: relative;
        align-items: center;
        padding: 12px 32px;
    }
</style>
