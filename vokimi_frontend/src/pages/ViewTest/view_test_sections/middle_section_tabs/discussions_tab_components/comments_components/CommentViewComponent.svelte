<script lang="ts">
  import type { TestDiscussionCommentVm } from "../../../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestDiscussionsTabData";
  import { ImgUtils } from "../../../../../../ts/utils/ImgUtils";
  import CommentsListViewComponent from "./CommentsListViewComponent.svelte";
  import CommentFooter from "./CommentFooter.svelte";

  export let comment: TestDiscussionCommentVm;
</script>

<div class="comment">
  <div class="comment-header">
    <img
      src={ImgUtils.imgUrl(comment.authorProfilePicture)}
      alt="Profile Picture"
      class="profile-picture"
    />
    <a href="/user/{comment.authorId}" class="author-link">
      {comment.authorUsername}
    </a>
    <span class="comment-date">{comment.createdAtDateTime}</span>
  </div>
  <div class="comment-body">
    <p>{comment.text}</p>
  </div>
  <CommentFooter
    viewersVoteIsUp={comment.isViewersVoteUp}
    votesRating={comment.votesRating}
    totalVotesCount={comment.totalVotesCount}
    commentId={comment.commentId}
  />
</div>
<CommentsListViewComponent comments={comment.childVms} />

<style>
  .comment {
    display: grid;
    grid-template-rows: 48px auto auto;
    border: 2px solid var(--back-secondary);
  }
  .comment-header {
    display: grid;
    grid-template-columns: 48px 1fr auto;
    align-items: center;
    gap: 16px;
    margin-left: 20px;
  }

  .profile-picture {
    width: 100%;
    aspect-ratio: 1/1;
    border-radius: 40%;
    border: 2px solid var(--back-secondary);
  }
  .author-link {
    
  }
  .comment-date {
    
  }
</style>
