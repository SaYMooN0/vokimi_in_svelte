<script lang="ts">
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { PublishedTestBriefInfo } from "../../../../ts/page_classes/my_tests_page/PublishedTestBriefInfo";
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
                        dto.averageRating,
                        dto.commentsCount,
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
    <div class="draft-tests-container">
        {#each publishedTests as test}
            <a href="/view-test/{test.id}">
                {test.name}
            </a>
        {/each}
    </div>
</TestsTabContentWrapper>
