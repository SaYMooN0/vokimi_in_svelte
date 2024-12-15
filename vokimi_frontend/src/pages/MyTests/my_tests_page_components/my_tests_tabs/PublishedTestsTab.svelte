<script lang="ts">
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { PublishedTestBriefInfo } from "../../../../ts/page_classes/my_tests_page/PublishedTestBriefInfo";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import TestsTabContentWrapper from "./TestsTabContentWrapper.svelte";
    export let publishedTests: PublishedTestBriefInfo[] = [];

    async function fetchPublishedTests(skipLengthCheck: boolean): Promise<Err> {
        if (publishedTests.length > 0 && !skipLengthCheck) {
            return Err.none();
        }

        const response = await fetch(
            "/api/userTests/getUserPublishedTestsBriefInfo",
        );
        if (response.status == 200) {
            const data = await response.json();
            publishedTests = data.map(
                (dto: any) =>
                    new PublishedTestBriefInfo(
                        dto.id,
                        dto.name,
                        dto.cover,
                        dto.template,
                        dto.publishedDate,
                        dto.takersCount,
                    ),
            );
            return Err.none();
        } else if (response.status == 404) {
            return new Err(await getErrorFromResponse(response));
        } else {
            publishedTests = [];
            return new Err("Something went wrong...");
        }
    }
</script>

<TestsTabContentWrapper
    fetchTestsFunc={fetchPublishedTests}
    yourTestsLabel="Your published tests:"
>
    <div class="published-tests-container">
        {#each publishedTests as test}
            <div class="published-test-view">
                <img
                    class="test-cover"
                    src={ImgUtils.imgUrl(test.cover)}
                    alt="test cover"
                />
                <div>
                    <span class="test-name">
                        {test.name}
                    </span>
                    <a href="/manage-test/{test.id}" class="manage-test-link">
                        Manage test
                    </a>
                    <a href="/view-test/{test.id}" class="view-test-link">
                        To the test page
                    </a>
                </div>
            </div>
        {/each}
    </div>
</TestsTabContentWrapper>

<style>
    .published-tests-container {
        display: flex;
        flex-direction: column;
    }
    .published-test-view {
        display: grid;
        grid-template-columns: 280px 1fr;
    }
    .published-test-view .test-cover {
        width: 100%;
        max-height: 320px;
        object-fit: cover;
        border-radius: 12px;
    }
</style>
