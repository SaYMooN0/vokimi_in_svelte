<script lang="ts">
  import type { TestDiscussionCommentVm } from "../../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestDiscussionsTabData";
  import CommentViewComponent from "./CommentViewComponent.svelte";

  export let comments: TestDiscussionCommentVm[];
</script>

<div class="comment-list">
  {#each comments as comment}
    <CommentViewComponent {comment} />
  {/each}
</div>

<style>
  .comment-list {
    display: flex;
    flex-direction: column;
    margin-top: 10px;
  }
</style>
