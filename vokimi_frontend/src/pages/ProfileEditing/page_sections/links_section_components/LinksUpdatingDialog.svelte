<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import type { EditPageLinksSectionData } from "../../../../ts/page_classes/profile_edit_page/EditPageLinksSectionData";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "../main_info_section_components/left_side_components/UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let errorMessage: string = "";

    export let updateParentElement: (newVal: EditPageLinksSectionData) => void;
    export let currentLinksData: EditPageLinksSectionData;
    let linksDataToEdit: Map<string, string | null> = new Map();
    export function open() {
        linksDataToEdit = currentLinksData.toDictionary();
        errorMessage = "";
        dialogElement.open();
    }

    async function saveNewLinks() {
        const validationErr = checkLinksForErr();
        if (validationErr.notNone()) {
            errorMessage = validationErr.toString();
            return;
        }
        errorMessage = "";
        const response = await fetch("/api/profileEditing/updateLinks", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(currentLinksData),
        });

        if (response.ok) {
            updateParentElement(currentLinksData);
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
    function checkLinksForErr(): Err {
        for (const [linkName, linkVal] of linksDataToEdit) {
            if (!StringUtils.isNullOrWhiteSpace(linkVal)) {
                if (
                    !linkVal?.startsWith("http://") &&
                    !linkVal?.startsWith("https://")
                ) {
                    return new Err(
                        `Links must start with http:// or https://. Link for  ${linkName} is invalid.`,
                    );
                }
            }
        }
        return Err.none();
    }
    function updateLinkValue(linkName: string, event: Event) {
        const target = event.target as HTMLInputElement;
        if (target) {
            linksDataToEdit.set(linkName, target.value);
        }
    }
</script>

<BaseDialog dialogId="linksEditingDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton
            onClose={() => {
                dialogElement.close();
            }}
        />
        <p class="dialog-title-p">My links:</p>
        <div class="links-container">
            {#each linksDataToEdit as [linkName, linkVal]}
                <span class="link-name">{linkName}: </span>
                <input
                    placeholder="https://..."
                    class="link-val"
                    type="text"
                    on:input={(e) => updateLinkValue(linkName, e)}
                />
            {/each}
        </div>
        {#if !StringUtils.isNullOrWhiteSpace(errorMessage)}
            <p class="error-message">{errorMessage}</p>
        {/if}
        <UpdateDialogSaveBtn onSaveClicked={saveNewLinks} />
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: flex;
        flex-direction: column;
        position: relative;
        align-items: center;
        padding: 12px 48px;
    }
    .dialog-title-p {
        margin: 8px 20px;
        font-size: 20px;
        color: var(--text);
    }

    .error-message {
        margin: 8px 0;
        color: var(--red-del);
    }
    .links-container {
        display: grid;
        grid-template-columns: auto 420px;
        row-gap: 12px;
        column-gap: 24px;
        font-size: 20px;
    }
    .link-name {
        font-size: inherit;
    }
    .link-val {
        font-size: inherit;
        outline: none;
        border: 2px solid var(--back-secondary);
        box-sizing: border-box;
        padding: 2px 4px;
        border-radius: 4px;
        background-color: var(--back-secondary);
        color: var(--text);
        width: 100%;
    }
    .link-val:focus {
        border-color: var(--primary);
    }
    .link-val::placeholder {
        font-size: 14px;
        color: var(--text-faded);
    }
</style>
