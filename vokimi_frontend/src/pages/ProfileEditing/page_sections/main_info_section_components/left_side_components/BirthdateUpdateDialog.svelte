<script lang="ts">
    import { backIn } from "svelte/easing";
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import CustomCheckbox from "../../../../../components/shared/CustomCheckbox.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { getAuthDataForced } from "../../../../../ts/stores/authStore";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "./UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let newBirthdate: Date | null;
    let doNotSet: boolean = false;
    let errorMessage: string = "";

    export let updateParentElement: (newVal: Date | null) => void;
    export function open() {
        newBirthdate = null;
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewBirthdate() {
        if (!doNotSet && newBirthdate === null) {
            errorMessage =
                "Please select the date of your birthdate or check the 'I do not want to' box";
            return;
        }
        const newVal = doNotSet ? null : newBirthdate;
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateBirthdate", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newVal),
        });

        if (response.ok) {
            await getAuthDataForced();
            updateParentElement(newVal);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
</script>

<BaseDialog dialogId="birthdateEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <h1 class="dialog-title">Set your birthdate</h1>
        {#if doNotSet}
            <input
                class="birhtdate-input disabled"
                type="date"
                bind:value={newBirthdate}
                disabled
            />
        {:else}
            <input
                class="birhtdate-input"
                type="date"
                bind:value={newBirthdate}
            />
        {/if}

        <p class="do-not-set-p">
            I don't want to set my birthdate:
            <CustomCheckbox bind:isChecked={doNotSet} />
        </p>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <UpdateDialogSaveBtn onSaveClicked={saveNewBirthdate} />
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

    .dialog-title {
        margin: 4px 24px;
        font-size: 28px;
        color: var(--text);
    }
    .birthdate-input{
        
    }
    .birthdate-input.disabled{
        
    }
    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
</style>
