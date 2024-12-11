<script lang="ts">
    export let userId: string;
    export let publishedTestsCount: number | null;
    export let followingsCount: number | null;
    export let friendsCount: number | null;
    export let followersCount: number | null;
</script>

<div class="account-base-stats">
    {#if publishedTestsCount !== null}
        <div class="stat-label">Published tests: {publishedTestsCount}</div>
    {/if}
    {#if followingsCount !== null}
        <div class="stat-label">Followings: {followingsCount}</div>
    {/if}
    {#if friendsCount !== null}
        <div class="stat-label">Friends: {friendsCount}</div>
    {/if}
    {#if followersCount !== null}
        <div class="stat-label">Followers: {followersCount}</div>
    {/if}
</div>
