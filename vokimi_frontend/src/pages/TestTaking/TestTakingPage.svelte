<script lang="ts">
    import TestAccessPageContainer from "../../components/test_access_page_container/TestAccessPageContainer.svelte";
    import {
        TestTemplate,
        TestTemplateUtils,
    } from "../../ts/enums/TestTemplate";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import { GeneralTestTakingData } from "../../ts/page_classes/test_taking_page/general_test/GeneralTestTakingData";
    import TestTakingForGeneralTemplate from "./test_taking_components/template_based_test_taking/TestTakingForGeneralTemplate.svelte";
    export let testId: string;

    async function loadTestTakingData(): Promise<Err | GeneralTestTakingData> {
        console.log(testId);
        const response = await fetch(
            "/api/testTaking/loadTestTakingData/" + testId,
        );
        if (response.ok) {
            const data = await response.json();
            const t: TestTemplate = TestTemplateUtils.fromId(data.testTemplate);
            switch (t) {
                case TestTemplate.General:
                    return new GeneralTestTakingData(
                        data.testName,
                        data.testTemplate,
                        data.testDescription,
                        data.testQuestions,
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
        {#if loadDataRes instanceof Err}
            <div class="loading-err-div">
                <p class="loading-err-p">{loadDataRes.toString()}</p>
            </div>
        {:else if loadDataRes instanceof GeneralTestTakingData}
            <TestTakingForGeneralTemplate testTakingData={loadDataRes} />
        {:else}
            <p>Not implemented test template</p>
        {/if}
    {/await}
</TestAccessPageContainer>

<style>
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
