<script lang="ts">
    import type { TestCreationConclusionTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationConclusionTabData";
    import type { TestCreationStylesTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import AccentColorPicker from "./editing_dialog_components/AccentColorPicker.svelte";
    import ArrowsTypePicker from "./editing_dialog_components/ArrowsTypePicker.svelte";

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
        const response = await fetch("/api/testStyles/updateDraftTestStyles", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(stylesDataToEdit),
        });
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
    <div class="dialog-content">
        <h1 class="dialog-title">Edit of the test styles</h1>
        <label class="set-default-btn" on:click={setDefaultValues}>
            Change back to default
        </label>

        <p class="choose-p">Choose an accent color</p>
        <AccentColorPicker bind:accentColor={stylesDataToEdit.accentColor} />
        <p class="choose-p">
            Choose the arrows that will be on the previous and next buttons
        </p>
        <ArrowsTypePicker bind:chosenType={stylesDataToEdit.arrowType} />
    </div>
</BaseDraftTestEditingDialog>

<style>
</style>
