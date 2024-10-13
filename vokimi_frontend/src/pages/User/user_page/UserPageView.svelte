<script lang="ts">
    import UserAdditionalInfoDialog from "../states_shared/UserAdditionalInfoDialog.svelte";
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";

    export let pageOwnerId: string;
    let dialogElement: UserAdditionalInfoDialog;
</script>

<UserAdditionalInfoDialog bind:this={dialogElement} userId={pageOwnerId} />
<UserPageViewFrame>
    <UserPageTopInfo
        userId={pageOwnerId}
        openUserAdditionalInfoDialog={() => dialogElement.open()}
    >
        <div slot="right-side-slot">
            <button class="edit-profile-btn">Follow</button>
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>
