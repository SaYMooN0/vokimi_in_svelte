<script lang="ts">
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";

    export let userId: string = "";
</script>

<UserPageViewFrame>
    <UserPageTopInfo {userId} />
</UserPageViewFrame>
