<script lang="ts">
    import type { TestCreationConclusionTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import type { TestCreationStylesTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";

    export let updateParentElementData: () => void;

    let stylesDataToEdit: TestCreationStylesTabData;
    let defaultStylesData: TestCreationStylesTabData | null = null;
    let dialogElement: BaseDraftTestEditingDialog;

    export function open(styles: TestCreationStylesTabData) {
        stylesDataToEdit = styles.copy();
        dialogElement.setErrorMessage("");
        dialogElement.open();
    }

    async function saveData() {
        const response = await fetch(
            "/api/testStyles/updateDraftTestStyles",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(stylesDataToEdit),
            },
        );
        if (response.ok) {
            await updateParentElementData();
            dialogElement.close();
        } else if (response.status === 400) {
            const data = await response.json();
            dialogElement.setErrorMessage(data.error);
        } else {
            dialogElement.setErrorMessage("Unknown error");
        }
    }
    async function setDefaultValues() {
        if (defaultStylesData === null) {
            const response = await fetch(
                "/api/testStyles/getDefaultStylesData",
            );
            if (response.ok) {
                defaultStylesData = await response.json();
            } else {
                dialogElement.setErrorMessage("Unknown error");
                return;
            }
        }
        if (defaultStylesData) {
            stylesDataToEdit = defaultStylesData.copy();
        }
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    <div class="dialog-content"></div>
</BaseDraftTestEditingDialog>

<style>
</style>
