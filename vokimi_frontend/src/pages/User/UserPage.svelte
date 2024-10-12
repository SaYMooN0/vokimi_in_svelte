<script lang="ts">
    import getAuthData from "../../ts/stores/authStore";
    import { StringUtils } from "../../ts/utils/StringUtils";
    import MyUserPageView from "./my_user_page/MyUserPageView.svelte";
    import UserPageView from "./user_page/UserPageView.svelte";
    enum AccPageState {
        MyPageViewer,
        UserPage,
        UserNotFound,
        DataFetchingErr,
    }

    export let userId: string;
    let accPageState: AccPageState;
    async function setAccPageState() {
        const authData = await getAuthData();
        if (StringUtils.isNullOrWhiteSpace(userId)) {
            if (authData !== null) {
                accPageState = AccPageState.MyPageViewer;
                userId = authData.UserId ?? "";
            } else {
                accPageState = AccPageState.UserNotFound;
            }
            return;
        }
        const response = await fetch("/api/users/doesUserExist/" + userId);
        if (response.ok) {
            const data = await response.json();
            if (data.exists) {
                if (userId === authData?.UserId) {
                    accPageState = AccPageState.MyPageViewer;
                } else {
                    accPageState = AccPageState.UserPage;
                }
            } else {
                accPageState = AccPageState.UserNotFound;
            }
        } else {
            accPageState = AccPageState.DataFetchingErr;
        }
    }
</script>

{#await setAccPageState()}
    <p>Loading...</p>
{:then}
    {#if accPageState === AccPageState.MyPageViewer}
        <MyUserPageView {userId} />
    {:else if accPageState === AccPageState.UserPage}
        <UserPageView pageOwnerId={userId} />
    {:else if accPageState === AccPageState.UserNotFound}
        <div class="user-not-found">User not found</div>
    {:else if accPageState === AccPageState.DataFetchingErr}
        <div class="unable-to-fetch">Unable to fetch data</div>
    {/if}
{/await}
