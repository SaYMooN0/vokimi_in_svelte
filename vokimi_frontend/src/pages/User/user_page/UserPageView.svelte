<script lang="ts">
    import UserAdditionalInfoDialog from "../states_shared/UserAdditionalInfoDialog.svelte";
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";

    export let pageOwnerId: string;
    let dialogElement: UserAdditionalInfoDialog;
    let viewerFollowsOwner: boolean;
    let ownerFollowsViewer: boolean;

    async function fetchViewerAndUserRelations() {
        const response = await fetch(
            `/api/userPage/getViewerAndUserRelations/${pageOwnerId}`,
        );
        if (response.ok) {
        } else if (response.status === 400) {
        } else {
        }
    }
</script>

<UserAdditionalInfoDialog bind:this={dialogElement} userId={pageOwnerId} />
<UserPageViewFrame>
    <UserPageTopInfo
        userId={pageOwnerId}
        openUserAdditionalInfoDialog={() => dialogElement.open()}
    >
        <div slot="right-side-slot">
            {#await fetchViewerAndUserRelations() then _}
                {#if viewerFollowsOwner}
                    <button class="follow-btn">Followed</button>
                    <div class="followed-context-menu">
                        <button>Unfollow</button>
                    </div>
                {:else}
                    <button class="follow-btn">Follow</button>
                {/if}
                {#if ownerFollowsViewer}
                    <span>User follows you</span>
                {/if}
            {/await}
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>
