<script lang="ts">
    export let discussionsCount: number;
    export let totalCommentsCount: number;
    export function incrementDiscussionsCount() {
        discussionsCount++;
    }
    export function incrementTotalCommentsCount() {
        totalCommentsCount++;
    }
</script>

<div class="discussions-count-panel">
    <div class="discussions-count">
        Discussions: {discussionsCount}
    </div>
    <div class="comments-count">
        Comments: {totalCommentsCount}
    </div>
</div>

<style>
    .discussions-count-panel {
        display: grid;
        grid-template-columns: 1fr 1fr;
    }

    .discussions-count-panel div {
        display: flex;
        justify-content: center;
        align-items: center;
    }
</style>
