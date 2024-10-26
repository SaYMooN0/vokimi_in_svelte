<script lang="ts">
    import type { Err } from "../../../../../ts/Err";
    import { DraftGeneralTestResultEditingData } from "../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/results/DraftGeneralTestResultEditingData";
    import { ImgUtils } from "../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";
    import BaseDraftTestEditingDialog from "../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import TextWithOptionalImageInput from "../../../creation_shared_components/TextWithOptionalImageInput.svelte";

    export let updateParentElementData: () => void;

    let fetchingDataErr: string = "";
    let resultId: string = "";
    let resultData: DraftGeneralTestResultEditingData;
    let dialogElement: BaseDraftTestEditingDialog;

    export async function open(resultIdVal: string) {
        fetchingDataErr = "";
        resultId = resultIdVal;
        resultNameInputId = "result-name-" + StringUtils.randomString(8);
        const url =
            "/api/testCreation/general/getDraftGeneralTestResultDataToEdit/" +
            resultId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            resultData = new DraftGeneralTestResultEditingData(
                data.id,
                data.name,
                data.text,
                data.imagePath,
            );
        } else if (response.status === 400) {
            const data = await response.json();
            fetchingDataErr = data.error;
        } else {
            fetchingDataErr = "Unknown error";
        }
        dialogElement.open();
    }

    async function saveData() {
        const dataErr: string | null = checkFormDataForError();
        if (dataErr !== null) {
            dialogElement.setErrorMessage(dataErr);
            return;
        }
        const response = await fetch(
            "/api/testCreation/general/saveChangesForDraftGeneralTestResult",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(resultData),
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
    function checkFormDataForError(): string | null {
        return null;
    }
    async function saveResultImage(file: File): Promise<string | Err> {
        return await ImgUtils.saveImage(
            file,
            `saveDraftGeneralTestResultImage/${resultId}`,
        );
    }
    let resultNameInputId: string;
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    {#if fetchingDataErr === ""}
        <div class="dialog-content">
            <p class="result-name-input-p">
                <label class="result-name-label" for={resultNameInputId}>
                    Result name:
                </label>
                <input
                    id={resultNameInputId}
                    bind:value={resultData.name}
                    placeholder="result name..."
                />
            </p>
            <TextWithOptionalImageInput
                bind:text={resultData.text}
                bind:imagePath={resultData.imagePath}
                textInputLabel="Result Text"
                saveImageFunction={saveResultImage}
            />
        </div>
    {:else}
        <p>{fetchingDataErr}</p>
    {/if}
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: 1200px;
        padding: 10px 20px;
        max-height: 76vh;
        box-sizing: border-box;
        overflow-y: auto;
        overflow-x: hidden;
    }
    .result-name-input-p {
        width: 100%;
        display: grid;
        grid-template-columns: auto 1fr;
        gap: 8px;
    }
    .result-name-input-p label {
        font-size: 24px;
    }
    .result-name-input-p input {
        font-size: 22px;
        outline: none;
        background-color: var(--back-secondary);
        border: 2px solid var(--back-secondary);
        border-radius: 4px;
        padding: 2px 6px;
        box-sizing: border-box;
        color: var(--text);
    }
    .result-name-input-p input:focus {
        border-color: var(--primary);
    }
</style>
