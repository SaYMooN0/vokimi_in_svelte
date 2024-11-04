<script lang="ts">
    import TestAccessPageContainer from "../../components/test_access_page_container/TestAccessPageContainer.svelte";
    import { TestTemplate } from "../../ts/enums/TestTemplate";
    import GeneralTestReceivedResultView from "./results_view_components/GeneralTestReceivedResultView.svelte";

    export let testId: string;
    export let receivedResId: string;
    export let templateStringId: string;
</script>

<TestAccessPageContainer {testId}>
    {#if templateStringId == TestTemplate.General}
        <GeneralTestReceivedResultView {receivedResId} />
    {:else}
        <p>Not implemented</p>
    {/if}
</TestAccessPageContainer>
