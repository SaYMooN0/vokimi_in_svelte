<script lang="ts">
    import { TestCreationStylesTabData } from "../../../../ts/test_creation_tabs_classes/test_creation_shared/TestCreationStylesTabData";
    import BaseDraftTestEditingDialog from "../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import AccentColorPicker from "./editing_dialog_components/AccentColorPicker.svelte";
    import ArrowsTypePicker from "./editing_dialog_components/ArrowsTypePicker.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    let stylesDataToEdit: TestCreationStylesTabData;
    let defaultStylesData: TestCreationStylesTabData | null = null;
    let dialogElement: BaseDraftTestEditingDialog;

    export function open(styles: TestCreationStylesTabData) {
        stylesDataToEdit = styles.copy();
        dialogElement.setErrorMessage("");
        dialogElement.open();
    }

    async function saveData() {
        const url = "/api/testStyles/updateDraftTestStyles/" + testId;
        console.log(url);
        const response = await fetch(url, {
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
            dialogElement.setErrorMessage("Unknown server error");
        }
    }
    async function setDefaultValues() {
        if (defaultStylesData === null) {
            const response = await fetch(
                "/api/testStyles/getDefaultStylesData",
            );
            if (response.ok) {
                const data = await response.json();
                defaultStylesData = new TestCreationStylesTabData(
                    data.accentColor,
                    data.arrowType,
                );
            } else {
                dialogElement.setErrorMessage(
                    "An error occurred during setting styles to default. Try again later",
                );
                return;
            }
        }
        if (defaultStylesData) {
            stylesDataToEdit = defaultStylesData.copy();
            console.log(stylesDataToEdit);
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
    .dialog-content {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    .dialog-title {
        margin-top: 20px;
        margin-bottom: 4px;
    }
    .set-default-btn {
        margin-bottom: 4px;
        cursor: pointer;
        font-size: 18px;
        color: var(--text-faded);
    }
    .set-default-btn:hover {
        color: var(--primary);
        text-decoration: underline;
    }
    .choose-p {
        margin-top: 16px;
        margin-bottom: 8px;
        color: var(--text);
        font-size: 24px;
    }
</style>
