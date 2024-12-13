<script lang="ts">
    export let userId: string;
    export let publishedTestsCount: number | null;
    export let followingsCount: number | null;
    export let friendsCount: number | null;
    export let followersCount: number | null;
</script>

<div class="account-base-stats unselectable">
    {#if publishedTestsCount !== null}
        <div class="stat-label">
            Published tests
            <span class="stat-value">{publishedTestsCount}</span>
        </div>
    {/if}
    {#if followingsCount !== null}
        <div class="stat-label">
            Followings
            <span class="stat-value">{followingsCount}</span>
        </div>
    {/if}
    {#if friendsCount !== null}
        <div class="stat-label">
            Friends
            <span class="stat-value">{friendsCount}</span>
        </div>
    {/if}
    {#if followersCount !== null}
        <div class="stat-label">
            Followers
            <span class="stat-value">{followersCount}</span>
        </div>
    {/if}
</div>

<style>
    .account-base-stats {
        margin-top: 12px;
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: center;
        gap: 12px;
    }
    .stat-label {
        width: 160px;
        height: 80px;
        background-color: var(--back-secondary);
        border-radius: 12px;
        padding: 12px;
        box-sizing: border-box;
        display: grid;
        align-items: center;
        justify-content: center;
        text-align: center;
        grid-template-rows: 1fr 40px;
        font-size: 18px;
        color: var(--primary);
        font-weight: 500;
        transition: all 0.12s ease-in;
        cursor: pointer;
        box-shadow: rgba(70, 50, 120, 0.1) 0px 2px 2px 0px;
    }

    .stat-label:hover {
        color: var(--primary-hov);
        transform: translateY(-8px) scale(1.04);
        box-shadow: rgba(100, 60, 140, 0.14) 0px 3px 4px 0px;
    }
    .stat-label:active {
        transform: translateY(-4px) scale(1.02);
        box-shadow: rgba(60, 20, 110, 0.1) 0px 2px 2px 0px;

        
    }
    .stat-value {
        cursor: inherit;
        color: inherit;
        font-size: 28px;
    }
</style>
