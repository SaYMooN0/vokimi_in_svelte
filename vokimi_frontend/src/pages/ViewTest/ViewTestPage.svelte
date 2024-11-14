<script lang="ts">
    import TestAccessPageContainer from "../../components/test_access_page_container/TestAccessPageContainer.svelte";
    import { Err } from "../../ts/Err";
    import { getErrorFromResponse } from "../../ts/ErrorResponse";
    import { ViewTestBaseInfoTabData } from "../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestBaseInfoTabData";
    import { TestTemplateUtils } from "../../ts/enums/TestTemplate";
    import { LanguageUtils } from "../../ts/enums/Language";
    import { TestNameAndCreatorSectionClass } from "../../ts/page_classes/view_test_page_classes/TestNameAndCreatorSectionClass";
    import TestPageContentTestCoverSection from "./view_test_sections/TestPageContentTestCoverSection.svelte";
    import TestPageTestNameAndCreatorSection from "./view_test_sections/TestPageTestNameAndCreatorSection.svelte";
    import TestPageMiddleSection from "./view_test_sections/TestPageMiddleSection.svelte";

    export let testId: string;
    export let startingTab: string | undefined = undefined;
    
    interface LoadInfoSuccess {
        testNameAndCreatorSection: TestNameAndCreatorSectionClass;
        testCoverPath: string;
        testMiddleSection: ViewTestBaseInfoTabData;
    }

    async function loadBasicTestInfo(): Promise<Err | LoadInfoSuccess> {
        const response = await fetch(
            "/api/viewTest/getBasicTestInfo/" + testId,
        );

        if (response.ok) {
            const data = await response.json();
            return {
                testNameAndCreatorSection: new TestNameAndCreatorSectionClass(
                    data.testName,
                    data.testCreatorUsername,
                    data.creatorId,
                ),
                testCoverPath: data.testCoverPath,
                testMiddleSection: new ViewTestBaseInfoTabData(
                    data.testDescription,
                    TestTemplateUtils.fromId(data.template),
                    LanguageUtils.fromId(data.language),
                    data.tags,
                ),
            };
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
    {#await loadBasicTestInfo()}
        <div class="loading-message-div">Preparing test page</div>
    {:then loadingResult}
        {#if loadingResult instanceof Err}
            <div class="data-loading-error">{loadingResult.toString()}</div>
        {:else}
            <div class="test-view-page-content">
                <TestPageContentTestCoverSection
                    {testId}
                    coverPath={loadingResult.testCoverPath}
                />
                <div class="view-middle-part">
                    <TestPageTestNameAndCreatorSection
                        sectionData={loadingResult.testNameAndCreatorSection}
                    />
                    <TestPageMiddleSection
                        testInfoTabData={loadingResult.testMiddleSection}
                        {testId}
                        {startingTab}
                    />
                </div>
            </div>
        {/if}
    {/await}
</TestAccessPageContainer>

<style>
    .test-view-page-content {
        margin: 20px auto;
        max-width: calc(74vw + 140px);
        display: grid;
        grid-template-columns: auto 1fr;
        gap: 30px;
    }
</style>
