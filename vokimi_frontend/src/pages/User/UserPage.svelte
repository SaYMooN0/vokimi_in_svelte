<script lang="ts">
    import getAuthData from "../../ts/stores/authStore";
    import { StringUtils } from "../../ts/utils/StringUtils";
    import MyUserPageView from "./my_user_page_view/MyUserPageView.svelte";
    import UserPageAuthenticatedView from "./user_page_authenticated_view/UserPageAuthenticatedView.svelte";
    import UserPageUnauthenticatedView from "./user_page_unauthenticated.view/UserPageUnauthenticatedView.svelte";
    export let userId: string;

    enum AccPageState {
        MyPageViewer,
        ViewerAuthenticated,
        ViewerUnauthenticated,
        UserNotFound,
        DataFetchingErr,
    }
    let accPageState: AccPageState;
    async function setAccPageState() {
        console.log("setAccPageState userId: " + userId);
        const authData = await getAuthData();
        if (StringUtils.isNullOrWhiteSpace(userId)) {
            if (authData !== null) {
                accPageState = AccPageState.MyPageViewer;
            } else {
                accPageState = AccPageState.UserNotFound;
            }
            return;
        }
        const response = await fetch("/api/users/doesUserExist/" + userId);
        if (response.ok) {
            const data = await response.json();
            if (data.exists) {
                if (authData !== null) {
                    accPageState = AccPageState.ViewerAuthenticated;
                } else {
                    accPageState = AccPageState.ViewerUnauthenticated;
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
        <MyUserPageView />
    {:else if accPageState === AccPageState.ViewerAuthenticated}
        <UserPageAuthenticatedView />
    {:else if accPageState === AccPageState.ViewerUnauthenticated}
        <UserPageUnauthenticatedView />
    {:else if accPageState === AccPageState.UserNotFound}
        <p>User not found</p>
    {:else if accPageState === AccPageState.DataFetchingErr}
        <div class="unable-to-fetch">Unable to fetch data</div>
    {/if}
{/await}
