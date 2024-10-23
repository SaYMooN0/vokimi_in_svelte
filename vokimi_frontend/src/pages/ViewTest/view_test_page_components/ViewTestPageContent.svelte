<script lang="ts">
    import TestPageContentTestCoverSection from "./TestPageContentTestCoverSection.svelte";
    import TestPageMiddleSection from "./TestPageMiddleSection.svelte";
    import TestPageTestNameAndCreatorSection from "./TestPageTestNameAndCreatorSection.svelte";

    export let testId: string;
</script>

<div class="test-view-page-content">
    <TestPageContentTestCoverSection {testId} />
    <div class="view-middle-part">
        <TestPageTestNameAndCreatorSection />
        <TestPageMiddleSection {testId} />
    </div>
</div>
