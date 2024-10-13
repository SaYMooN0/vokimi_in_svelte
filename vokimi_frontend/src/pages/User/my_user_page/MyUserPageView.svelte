<script lang="ts">
    import { Link } from "svelte-routing";
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
    <UserPageTopInfo
        {userId}
        openUserAdditionalInfoDialog={async () => await dialogElement.open()}
    >
        <div slot="right-side-slot" class="edit-profile-link">
            <Link to="/profile-editing">Edit profile</Link>
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>

<style>
    .edit-profile-link {
        padding: 12px 20px;
        background-color: var(--primary);
        color: var(--back-main);
    }
</style>
