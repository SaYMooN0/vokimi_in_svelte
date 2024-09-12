<script lang="ts">
    import BasicToolTip from "../../../../../../components/shared/BasicToolTip.svelte";
    import CustomCheckbox from "../../../../../../components/shared/CustomCheckbox.svelte";
    import {
        GeneralTestAnswerType,
        GeneralTestAnswerTypeUtils,
    } from "../../../../../../ts/enums/GeneralTestAnswerType";
    import { DraftGeneralTestQuestionEditingData } from "../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/DraftGenralTestQuestionEditingData";
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import TextWithOptionalImageInput from "../../../../creation_shared_components/TextWithOptionalImageInput.svelte";
    import DraftGeneralTestQuestionEditingMultipleChoiceZone from "../dialog_components/editing_dialog_zone_components/DraftGeneralTestQuestionEditingMultipleChoiceZone.svelte";
    import DraftGeneralTestQuestionEditingAnswersZone from "./editing_dialog_zone_components/DraftGeneralTestQuestionEditingAnswersZone.svelte";
    let fetchingDataErr: string = "";
    export let updateParentElementData: () => void;
    export let testId: string;
    export async function open(questionIdVal: string) {
        fetchingDataErr = "";
        questionId = questionIdVal;
        const url =
            "/api/testCreation/general/getDraftGeneralTestQuestionDataToEdit/" +
            questionId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            const answersType: GeneralTestAnswerType | null =
                GeneralTestAnswerTypeUtils.fromId(data.answersType);
            if (answersType === null) {
                fetchingDataErr = "Unknown error";
                return;
            }
            questionData = new DraftGeneralTestQuestionEditingData(
                data.id,
                data.text,
                data.imagePath,
                data.shuffleAnswers,
                answersType,
                data.minAnswersCount,
                data.maxAnswersCount,
                data.answers,
            );
        } else if (response.status === 400) {
            const data = await response.json();
            fetchingDataErr = data.error;
        } else {
            fetchingDataErr = "Unknown error";
        }
        dialogElement.open();
    }

    let questionId: string;
    let questionData: DraftGeneralTestQuestionEditingData;

    let dialogElement: BaseDraftTestEditingDialog;

    async function saveData() {
        const dataErr: string | null = checkFormDataForError();
        if (dataErr !== null) {
            dialogElement.setErrorMessage(dataErr);
            return;
        }
        const response = await fetch(
            "/api/testCreation/general/updateDraftGeneralTestQuestionData",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    questionId: questionId,
                }),
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
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    {#if fetchingDataErr === ""}
        <div class="dialog-content">
            <TextWithOptionalImageInput
                text={questionData.text}
                image={questionData.imagePath}
                textInputLabel="Question Text"
                saveImageFunction={async () => {
                    return "Not implemented";
                }}
            />
            <div class="input-label">
                Shuffle Answers:
                <BasicToolTip text={"Shuffle Answers"} />
                <CustomCheckbox isChecked={questionData.shuffleAnswers} />
            </div>

            <DraftGeneralTestQuestionEditingMultipleChoiceZone
                isMultiple={questionData.isMultiple}
                minAnswersCount={questionData.minAnswersCount}
                maxAnswersCount={questionData.maxAnswersCount}
            />
            <DraftGeneralTestQuestionEditingAnswersZone
                {testId}
                {questionId}
                answersType={questionData.answersType}
                answers={questionData.answers}
            />
        </div>
    {:else}
        <p>{fetchingDataErr}</p>
    {/if}
</BaseDraftTestEditingDialog>

<style>
    .dialog-content {
        width: 1200px;
        max-height: 76vh;
        box-sizing: border-box;
        overflow-y: auto;
        overflow-x: hidden;
    }
    .dialog-content :global(.input-label) {
        gap: 8px;
        font-size: 22px;
        display: flex;
        flex-direction: row;
        justify-content: left;
        align-items: center;
    }
</style>
