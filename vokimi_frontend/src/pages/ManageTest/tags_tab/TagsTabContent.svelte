<script lang="ts">
    import { Err } from "../../../ts/Err";
    import TabContentWrapper from "../page_layout/TabContentWrapper.svelte";
    import CurrentTestTagsView from "./CurrentTestTagsView.svelte";
    export let testId: string;
    export let isActive: boolean;

    interface TagsTabData {
        tags: string[];
    }

    async function fetchTabData(): Promise<TagsTabData | Err> {
        // const response = await fetch(`/api/manageTest/tags/tabData/${testId}`);
        return {
            tags: ["tag1", "tag2", "tag3"],
        };
    }
</script>

<TabContentWrapper {fetchTabData} let:tabDataSetRes {isActive}>
    {#if !(tabDataSetRes instanceof Err)}
        <CurrentTestTagsView tags={tabDataSetRes.tags} />
    {/if}
</TabContentWrapper>
