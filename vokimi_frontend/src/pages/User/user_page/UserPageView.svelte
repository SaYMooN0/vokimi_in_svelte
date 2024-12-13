<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import UserAdditionalInfoDialog from "../states_shared/UserAdditionalInfoDialog.svelte";
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";
    import FollowButton from "./top_info_components/FollowButton.svelte";
    import FollowedLabel from "./top_info_components/FollowedLabel.svelte";

    export let pageOwnerId: string;
    let dialogElement: UserAdditionalInfoDialog;
    let viewerFollowsOwner: boolean;
    let ownerFollowsViewer: boolean;
    let fetchingErr: string = "";

    async function fetchViewerAndUserRelations() {
        fetchingErr = "";
        const response = await fetch(
            `/api/userPage/getViewerAndUserRelations/${pageOwnerId}`,
        );
        if (response.ok) {
            const data = await response.json();
            viewerFollowsOwner = data.viewerFollowsUser;
            ownerFollowsViewer = data.userFollowsViewer;
            console.log(data);
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "Unable to fetch information";
        }
    }
    function updateFollowingStates(
        viewerFollowsUser: boolean,
        userFollowsViewer: boolean,
    ) {
        viewerFollowsOwner = viewerFollowsUser;
        ownerFollowsViewer = userFollowsViewer;
    }
</script>

<UserAdditionalInfoDialog bind:this={dialogElement} userId={pageOwnerId} />
<UserPageViewFrame>
    <UserPageTopInfo
        userId={pageOwnerId}
        openUserAdditionalInfoDialog={() => dialogElement.open()}
    >
        <div slot="right-side-slot" class="right-side-slot-div">
            {#await fetchViewerAndUserRelations() then _}
                {#if StringUtils.isNullOrWhiteSpace(fetchingErr)}
                    {#if viewerFollowsOwner}
                        <FollowedLabel userId={pageOwnerId} updateParentElement={updateFollowingStates} />
                    {:else}
                        <FollowButton userId={pageOwnerId} updateParentElement={updateFollowingStates} />
                    {/if}
                    {#if ownerFollowsViewer}
                        <span class="relations-span">
                            {viewerFollowsOwner
                                ? "You are friends"
                                : "User follows you"}
                        </span>
                    {/if}
                {:else}
                    <span class="fetching-err">
                        {fetchingErr}
                    </span>
                {/if}
            {/await}
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>

<style>
    .right-side-slot-div :global(button) {
        margin: 0 auto;
        display: grid;
        grid-template-columns: 24px auto;
        gap: 4px;
        color: var(--back-main);
        background-color: var(--primary);
        padding: 6px 16px;
        font-size: 20px;
        border-radius: 4px;
        outline: none;
        border: none;
        cursor: pointer;

    }
    .right-side-slot-div :global(button):active{
        transform: scale(0.98);
    }
    .relations-span {
        color: var(--text-faded) s;
        font-size: 16px;
    }
    .fetching-err {
        color: var(--red-del);
        font-weight: 500s;
    }
</style>
