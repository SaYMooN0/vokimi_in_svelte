<script lang="ts">
    import type { TestDiscussionCommentVm } from "../../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/TestDiscussionCommentVm";
  import CommentViewComponent from "./CommentViewComponent.svelte";

  export let incrementTotalCommentsCount: () => void;
  export let comments: TestDiscussionCommentVm[];
  export function updateCommentsList(newComments: TestDiscussionCommentVm[]) {
    newlyStartedDiscussion = [];
    comments = [...newComments];
  }
  export function addNewStartedDiscussion(commentVm: TestDiscussionCommentVm) {
    newlyStartedDiscussion = [commentVm, ...newlyStartedDiscussion];
  }
  let newlyStartedDiscussion: TestDiscussionCommentVm[] = [];
</script>

<div class="comment-list">
  {#each newlyStartedDiscussion as comment}
    <CommentViewComponent {comment} {incrementTotalCommentsCount} />
  {/each}
</div>

<div class="comment-list">
  {#each comments as comment}
    <CommentViewComponent {comment} {incrementTotalCommentsCount} />
  {/each}
</div>

<style>
  .comment-list {
    display: flex;
    flex-direction: column;
    margin-top: 4px;
  }
</style>
