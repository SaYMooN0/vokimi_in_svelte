<script lang="ts">
    import TestBriefInfoViewComponent from "../../../../components/shared/TestBriefInfoViewComponent.svelte";
import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { MyTestsPageTestViewModel } from "../../../../ts/MyTestsPageTestViewModel";
    import TestsTabContentWrapper from "./TestsTabContentWrapper.svelte";

    export let draftTests: MyTestsPageTestViewModel[] = [];
    async function fetchDraftTests(skipLengthCheck: boolean): Promise<Err> {
        if (draftTests.length > 0 && !skipLengthCheck) {
            return Err.none();
        }

        const response = await fetch("/api/tests/getUserDraftTestsBriefInfo");
        if (response.status == 200) {
            let j = await response.json();
            draftTests = j;
            return Err.none();
        } else if (response.status == 404) {
            return new Err(await getErrorFromResponse(response));
        } else {
            draftTests = [];
            return new Err("Something went wrong...");
        }
    }
</script>

<TestsTabContentWrapper
    fetchTestsFunc={fetchDraftTests}
    yourTestsLabel="Your draft tests:"
>
    {#each draftTests as test}
    <TestBriefInfoViewComponent/>
        <a href="/testCreation/{test.id}/main-info-view">
            <p>{test.name}</p>
        </a>
    {/each}
</TestsTabContentWrapper>
