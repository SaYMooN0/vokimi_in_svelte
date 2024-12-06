<script lang="ts">
    import BaseDialog from "../../../../components/BaseDialog.svelte";
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { EditPageLinksSectionData } from "../../../../ts/page_classes/profile_edit_page/EditPageLinksSectionData";
    import CloseButton from "../../../../components/shared/CloseButton.svelte";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import UpdateDialogSaveBtn from "../main_info_section_components/left_side_components/UpdateDialogSaveBtn.svelte";

    let dialogElement: BaseDialog;
    let errorMessage: string = "";

    export let updateParentElement: (newVal: EditPageLinksSectionData) => void;
    export let currentLinksData: EditPageLinksSectionData;
    let linksDataToEdit: { linkName: string; linkVal: string | null }[] = [];
    export function open() {
        linksDataToEdit = currentLinksData.toNameValList();
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
            body: JSON.stringify(
                EditPageLinksSectionData.fromNameValList(linksDataToEdit),
            ),
        });

        if (response.ok) {
            const responseData = await response.json();
            updateParentElement(new EditPageLinksSectionData(responseData));
            dialogElement.close();
        } else if (response.status === 400) {
            errorMessage = await getErrorFromResponse(response);
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
    function checkLinksForErr(): Err {
        for (const link of linksDataToEdit) {
            if (!StringUtils.isNullOrWhiteSpace(link.linkVal)) {
                if (
                    !link.linkVal?.startsWith("http://") &&
                    !link.linkVal?.startsWith("https://")
                ) {
                    return new Err(
                        `Links must start with http:// or https://. Link for  ${link.linkVal} is invalid.`,
                    );
                }
            }
        }
        return Err.none();
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
            {#each linksDataToEdit as linkData}
                <span class="link-name">{linkData.linkName}: </span>
                <input
                    placeholder="https://..."
                    class="link-val"
                    type="text"
                    bind:value={linkData.linkVal}
                />
                <svg
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 24 24"
                    fill="none"
                    class="clear-link-icon"
                    on:click={() => {
                        linkData.linkVal = null;
                    }}
                >
                    <path
                        d="M18 6L12 12M12 12L6 18M12 12L18 18M12 12L6 6"
                        stroke="currentColor"
                        stroke-width="2.4"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                    />
                </svg>
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
        grid-template-columns: auto 420px 24px;
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
    .clear-link-icon {
        width: 100%;
        aspect-ratio: 1/1;
        cursor: pointer;
        color: var(--back-main);
        background-color: var(--text-faded);
        border-radius: 4px;
        padding: 2px;
        box-sizing: border-box;
    }
    .clear-link-icon:hover {
        background-color: var(--primary);
    }
</style>
