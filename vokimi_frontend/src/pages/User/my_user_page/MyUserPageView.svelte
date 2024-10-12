<script lang="ts">
    import UserAdditionalInfoDialog from "../states_shared/UserAdditionalInfoDialog.svelte";
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";

    export let userId: string = "";
    let dialogElement: UserAdditionalInfoDialog;
</script>

<UserAdditionalInfoDialog bind:this={dialogElement} {userId}>
    <button class="edit-additional-info-btn">Edit</button>
</UserAdditionalInfoDialog>
<UserPageViewFrame>
    <UserPageTopInfo {userId} openUserAdditionalInfoDialog={dialogElement.open}>
        <div slot="banner-slot">
            <button class="edit-profile-btn">Edit profile</button>
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>
