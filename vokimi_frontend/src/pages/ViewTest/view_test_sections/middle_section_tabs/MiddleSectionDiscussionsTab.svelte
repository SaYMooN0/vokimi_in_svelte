<script lang="ts">
    import { Err } from "../../../../ts/Err";
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { ViewTestDiscussionsTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestDiscussionsTabData";
    import CommentsListViewComponent from "./discussions_tab_components/comments_components/CommentsListViewComponent.svelte";
    import NewDiscussionInput from "./discussions_tab_components/NewDiscussionInput.svelte";
    import TestDiscussionsCountPanel from "./discussions_tab_components/TestDiscussionsCountPanel.svelte";
    export let testId: string;

    async function fetchDiscussionTabData(): Promise<
        ViewTestDiscussionsTabData | Err
    > {
        const response = await fetch(
            `/api/viewTest/getTestDiscussionsInfo/${testId}`,
        );
        if (response.ok) {
            const data = await response.json();
            return new ViewTestDiscussionsTabData(
                data.discussionsCount,
                data.totalCommentsCount,
                data.comments,
            );
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("An unknown error occurred.");
        }
    }
</script>

{#await fetchDiscussionTabData() then fetchingRes}
    {#if fetchingRes instanceof Err}
        <p class="fetching-err">{fetchingRes.toString()}</p>
    {:else}
        <div class="discussions-tab-content">
            <NewDiscussionInput {testId} />
            <TestDiscussionsCountPanel
                discussionsCount={fetchingRes.discussionsCount}
                totalCommentsCount={fetchingRes.totalCommentsCount}
            />
            <CommentsListViewComponent comments={fetchingRes.comments} />
        </div>
    {/if}
{/await}

<style>
    .discussions-tab-content {
        display: flex;
        flex-direction: column;
    }
</style>
