<script lang="ts">
    import { GeneralTestAnswerType } from "../../../../../../../ts/enums/GeneralTestAnswerType";
    import { DraftGeneralTestImageOnlyAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestImageOnlyAnswerFormData";
    import { DraftGeneralTestTextAndImageAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextAndImageAnswerFormData";
    import { DraftGeneralTestTextOnlyAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/DraftGeneralTestTextOnlyAnswerFormData";
    import type { IDraftGeneralTestAnswerFormData } from "../../../../../../../ts/test_creation_tabs_classes/general_test_creation/draft_general_test_questions/answers/IDraftGeneralTestAnswerFormData";

    export let answers: IDraftGeneralTestAnswerFormData[];
    export let answersType: GeneralTestAnswerType;

    function addAnswer() {
        const newAnswer: IDraftGeneralTestAnswerFormData = (() => {
            switch (answersType) {
                case GeneralTestAnswerType.TextOnly:
                    return new DraftGeneralTestTextOnlyAnswerFormData("", {});
                case GeneralTestAnswerType.TextAndImage:
                    return new DraftGeneralTestTextAndImageAnswerFormData(
                        "",
                        "",
                        {},
                    );
                case GeneralTestAnswerType.ImageOnly:
                    return new DraftGeneralTestImageOnlyAnswerFormData("", {});
                default:
                    throw new Error("Unknown answer type");
            }
        })();
        answers = [...answers, newAnswer];
    }
</script>

<div class="answers-zone">
    <div class="answers-container">
        {#each answers as answer}
            {#if answer instanceof DraftGeneralTestTextOnlyAnswerFormData}
                <p>TextOnly</p>
            {:else if answer instanceof DraftGeneralTestTextAndImageAnswerFormData}
                <p>TextAndImage</p>
            {:else if answer instanceof DraftGeneralTestImageOnlyAnswerFormData}
                <p>ImageOnly</p>
            {:else}
                <p>Unknown answer form type</p>
            {/if}
        {/each}
    </div>
    <button class="add-answer-button" on:click={addAnswer}>Add Answer</button>
</div>
