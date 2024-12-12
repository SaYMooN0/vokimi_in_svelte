<script lang="ts">
    import { Link } from "svelte-routing";
    import UserAdditionalInfoDialog from "../states_shared/UserAdditionalInfoDialog.svelte";
    import UserPageTopInfo from "../states_shared/UserPageTopInfo.svelte";
    import UserPageViewFrame from "../states_shared/UserPageViewFrame.svelte";
    import AccountBaseStats from "../states_shared/AccountBaseStats.svelte";

    export let userId: string = "";
    let dialogElement: UserAdditionalInfoDialog;

    async function logout() {
        const response = await fetch("/api/logout", { method: "POST" });
        if (response.ok) {
            window.location.href = "/auth/login";
        }
    }
</script>

<UserAdditionalInfoDialog bind:this={dialogElement} {userId}>
    <div class="edit-additional-info-link">
        <Link to="/profile-editing/additional-info">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                color="#000000"
                fill="none"
            >
                <path
                    d="M16.4249 4.60509L17.4149 3.6151C18.2351 2.79497 19.5648 2.79497 20.3849 3.6151C21.205 4.43524 21.205 5.76493 20.3849 6.58507L19.3949 7.57506M16.4249 4.60509L9.76558 11.2644C9.25807 11.772 8.89804 12.4078 8.72397 13.1041L8 16L10.8959 15.276C11.5922 15.102 12.228 14.7419 12.7356 14.2344L19.3949 7.57506M16.4249 4.60509L19.3949 7.57506"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linejoin="round"
                />
                <path
                    d="M18.9999 13.5C18.9999 16.7875 18.9999 18.4312 18.092 19.5376C17.9258 19.7401 17.7401 19.9258 17.5375 20.092C16.4312 21 14.7874 21 11.4999 21H11C7.22876 21 5.34316 21 4.17159 19.8284C3.00003 18.6569 3 16.7712 3 13V12.5C3 9.21252 3 7.56879 3.90794 6.46244C4.07417 6.2599 4.2599 6.07417 4.46244 5.90794C5.56879 5 7.21252 5 10.5 5"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Edit
        </Link>
    </div>
</UserAdditionalInfoDialog>
<UserPageViewFrame>
    <UserPageTopInfo
        {userId}
        openUserAdditionalInfoDialog={async () => await dialogElement.open()}
    >
        <div slot="right-side-slot" class="my-page-right-side-slot">
            <Link to="/profile-editing" class="edit-profile-link">
                Edit my profile
            </Link>
            <label on:click={logout} class="logout-btn"> Log out </label>
        </div>
    </UserPageTopInfo>
</UserPageViewFrame>

<style>
    .my-page-right-side-slot {
        display: grid;
        grid-template-columns: auto auto;
        gap: 16px;
    }
    .my-page-right-side-slot :global(.edit-profile-link) {
        margin: 0 auto;
        color: var(--back-main);
        background-color: var(--primary);
        padding: 6px 16px;
        border-radius: 4px;
        font-size: 18px;
    }

    .my-page-right-side-slot :global(.edit-profile-link):hover {
        background-color: var(--primary-hov);
    }
    .logout-btn {
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: var(--red-del);
        color: var(--back-main);
        padding: 0 16px;
        border-radius: 4px;
        cursor: pointer;
    }

    .logout-btn:hover {
        background-color: var(--red-del-hov);
    }
    .edit-additional-info-link :global(a) {
        width: fit-content;
        margin: 0 auto;
        background-color: var(--primary);
        color: var(--back-main);
        display: flex;
        align-items: center;
        flex-direction: row;
        padding: 4px 12px;
        gap: 4px;
        border-radius: 4px;
        font-size: 20px;
    }
    .edit-additional-info-link svg {
        height: 26px;
        color: inherit;
    }
    .edit-additional-info-link :global(a):hover {
        background-color: var(--primary-hov);
    }
</style>
