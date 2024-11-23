<script lang="ts">
  import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
  import CommentFooter from "./CommentFooter.svelte";
  import CommentAnswerInput from "./CommentAnswerInput.svelte";
  import type { TestDiscussionCommentVm } from "../../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/discussions_tab_classes/TestDiscussionCommentVm";
  import CommentsListViewComponent from "./CommentsListViewComponent.svelte";
  import ProfilePictureWithLink from "../../tabs_shared/ProfilePictureWithLink.svelte";
  import UsernameWithLink from "../../tabs_shared/UsernameWithLink.svelte";

  export let comment: TestDiscussionCommentVm;
  export let incrementTotalCommentsCount: () => void;

  let answerInput: CommentAnswerInput;
  function addNewAnswer(answerComment: TestDiscussionCommentVm) {
    comment.childVms = [answerComment, ...comment.childVms];
    incrementTotalCommentsCount();
  }
</script>

<div class="comment">
  <div class="comment-header">
    <ProfilePictureWithLink
      userId={comment.authorId}
      profilePicturePath={comment.authorProfilePicture}
    />
    <UsernameWithLink
      userId={comment.authorId}
      username={comment.authorUsername}
    />
    <span class="comment-date">{comment.createdAtDateTime}</span>
  </div>
  <div class="comment-body">
    <p>{comment.text}</p>
  </div>
  <CommentFooter
    showAnswerInput={() => answerInput.show()}
    viewersVoteIsUp={comment.isViewersVoteUp}
    votesRating={comment.votesRating}
    totalVotesCount={comment.totalVotesCount}
    commentId={comment.commentId}
  />
</div>
<CommentAnswerInput
  showSavedAnswer={addNewAnswer}
  parentCommentId={comment.commentId}
  bind:this={answerInput}
/>
<div class="child-comments">
  <CommentsListViewComponent
    comments={comment.childVms}
    {incrementTotalCommentsCount}
  />
</div>

<style>
  .comment {
    display: grid;
    grid-template-rows: 48px auto auto;
    padding: 8px;
    border-radius: 12px;
    box-shadow:
      rgba(0, 0, 0, 0.05) 0px 4px 16px 0px,
      rgb(67, 58, 178, 0.1) 0px 0px 0px 1px;
  }
  .comment-header {
    display: grid;
    grid-template-columns: auto 1fr auto;
    align-items: center;
    gap: 8px;
    margin-left: 4px;
  }

  .comment-date {
    color: var(--text-faded);
    font-size: 14px;
  }
  .child-comments {
    margin-left: 24px;
  }
</style>
