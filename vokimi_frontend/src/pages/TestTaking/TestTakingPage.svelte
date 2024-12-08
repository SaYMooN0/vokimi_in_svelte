<script lang="ts">
    import TestAccessPageContainer from "../../components/test_access_page_container/TestAccessPageContainer.svelte";
    import {
        TestStylesArrowType,
        TestStylesArrowTypeUtils,
    } from "../../ts/enums/TestStylesArrowType";
    import {
        TestTemplate,
        TestTemplateUtils,
    } from "../../ts/enums/TestTemplate";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import { GeneralTestTakingData } from "../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import { GeneralTestTakingQuestionData } from "../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingQuestionData";
    import TestTakingForGeneralTemplate from "./test_taking_components/template_based_test_taking/TestTakingForGeneralTemplate.svelte";
    export let testId: string;

    async function loadTestTakingData(): Promise<Err | GeneralTestTakingData> {
        const response = await fetch(
            "/api/testTaking/loadTestTakingData/" + testId,
        );
        if (response.ok) {
            const data = JSON.parse(await response.json());
            const t: TestTemplate = TestTemplateUtils.fromId(data.testTemplate);
            switch (t) {
                case TestTemplate.General:
                    return new GeneralTestTakingData(
                        data.accentColor,
                        TestStylesArrowTypeUtils.fromId(data.arrowIcons),
                        data.conclusionData,
                        data.questions.sort(
                            (
                                a: GeneralTestTakingQuestionData,
                                b: GeneralTestTakingQuestionData,
                            ) => a.orderInTest - b.orderInTest,
                        ),
                    );
                case TestTemplate.Scoring:
                    return new Err("Scoring tests are not implemented yet");
                case TestTemplate.CorrectAnswers:
                    return new Err(
                        "Correct answers tests are not implemented yet",
                    );
                default:
                    return new Err(
                        "Something went wrong... Please refresh the page and try again",
                    );
            }
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err(
                "Something went wrong... Please refresh the page and try again",
            );
        }
    }
</script>

<TestAccessPageContainer {testId}>
    {#await loadTestTakingData() then loadDataRes}
        <div class="page-frame">
            {#if loadDataRes instanceof Err}
                <div class="loading-err-div">
                    <p class="loading-err-p">{loadDataRes.toString()}</p>
                </div>
            {:else if loadDataRes instanceof GeneralTestTakingData}
                <TestTakingForGeneralTemplate
                    {testId}
                    testTakingData={loadDataRes}
                />
            {:else}
                <p>Not implemented test template</p>
            {/if}
        </div>
    {/await}
</TestAccessPageContainer>

<style>
    .page-frame {
        margin: 20px auto;
        max-width: calc(74vw + 140px);
    }
    .loading-err-div {
        position: absolute;
        top: 40%;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        justify-content: center;
        align-items: center;
        background-color: var(--back-secondary);
    }
</style>
