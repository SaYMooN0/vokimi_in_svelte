<script lang="ts">
    import BasicToolTip from "../../../../../../components/shared/BasicToolTip.svelte";
    import CustomCheckbox from "../../../../../../components/shared/CustomCheckbox.svelte";
    import {
        GeneralTestAnswerType,
        GeneralTestAnswerTypeUtils,
    } from "../../../../../../ts/enums/GeneralTestAnswerType";
    import { Err } from "../../../../../../ts/Err";
    import { DraftGeneralTestQuestionEditingData } from "../../../../../../ts/page_classes/test_creation_page/test_creation_tabs_classes/general_test_creation/questions/DraftGeneralTestQuestionEditingData";
    import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../../../ts/utils/StringUtils";
    import BaseDraftTestEditingDialog from "../../../../creation_shared_components/editing_dialog_components/BaseDraftTestEditingDialog.svelte";
    import TextWithOptionalImageInput from "../../../../../../components/shared/TextWithOptionalImageInput.svelte";
    import DraftGeneralTestQuestionEditingMultipleChoiceZone from "../dialog_components/editing_dialog_zone_components/DraftGeneralTestQuestionEditingMultipleChoiceZone.svelte";
    import DraftGeneralTestQuestionEditingAnswersZone from "./editing_dialog_zone_components/DraftGeneralTestQuestionEditingAnswersZone.svelte";

    export let updateParentElementData: () => void;
    export let testId: string;

    let fetchingDataErr: string = "";
    let questionId: string;
    let questionData: DraftGeneralTestQuestionEditingData;
    let dialogElement: BaseDraftTestEditingDialog;

    export async function open(questionIdVal: string) {
        dialogElement.setErrorMessage("");
        fetchingDataErr = "";
        questionId = questionIdVal;
        const url =
            "/api/testCreation/general/getDraftGeneralTestQuestionDataToEdit/" +
            questionId;
        const response = await fetch(url);
        if (response.ok) {
            const data = JSON.parse(await response.json());
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
                data.maxAnswersForQuestionCount,
                data.maxRelatedResultsForAnswerCount,
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
        const dataErr: Err = checkFormDataForError();
        if (dataErr.notNone()) {
            dialogElement.setErrorMessage(dataErr.toString());
            return;
        }
        const url =
            "/api/testCreation/general/saveChangesForDraftGeneralTestQuestion/" +
            questionData.answersType;
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(questionData),
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
    function checkFormDataForError(): Err {
        if (StringUtils.isNullOrWhiteSpace(questionData.text)) {
            return new Err("Question text cannot be empty");
        }
        if (
            questionData.answers.length >
            questionData.maxAnswersForQuestionCount
        ) {
            return new Err(
                `Maximum answers count cannot be more than ${questionData.maxAnswersForQuestionCount}. 
                Current answers count is ${questionData.answers.length}`,
            );
        }
        if (questionData.isMultiple) {
            if (questionData.maxAnswersCount < questionData.minAnswersCount) {
                return new Err(
                    "Minimum answers count cannot be more than maximum answers count",
                );
            }
            if (questionData.answers.length < questionData.minAnswersCount) {
                return new Err(
                    "Minimum answers count cannot be less than total number of answers",
                );
            }
            if (questionData.maxAnswersCount > questionData.answers.length) {
     
                return new Err(
                    "Maximum answers count cannot be more than total number of answers",
                );
            }
        }
        return Err.none();
    }
    async function saveQuestionImage(file: File): Promise<string | Err> {
        return await ImgUtils.saveImage(
            file,
            `saveDraftGeneralTestQuestionImage/${questionId}`,
        );
    }
</script>

<BaseDraftTestEditingDialog
    onSaveButtonClicked={saveData}
    bind:this={dialogElement}
>
    {#if fetchingDataErr === ""}
        <div class="dialog-content">
            <TextWithOptionalImageInput
                bind:text={questionData.text}
                bind:imagePath={questionData.imagePath}
                textInputLabel="Question Text"
                saveImageFunction={saveQuestionImage}
            />
            <div class="input-label">
                Shuffle Answers:
                <BasicToolTip text={"Shuffle Answers"} />
                <CustomCheckbox bind:isChecked={questionData.shuffleAnswers} />
            </div>

            <DraftGeneralTestQuestionEditingMultipleChoiceZone
                bind:isMultiple={questionData.isMultiple}
                bind:minAnswersCount={questionData.minAnswersCount}
                bind:maxAnswersCount={questionData.maxAnswersCount}
            />
            <DraftGeneralTestQuestionEditingAnswersZone
                maxAnswersForQuestionCount={questionData.maxAnswersForQuestionCount}
                maxRelatedResultsForAnswerCount={questionData.maxRelatedResultsForAnswerCount}
                {testId}
                {questionId}
                answersType={questionData.answersType}
                bind:answers={questionData.answers}
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
