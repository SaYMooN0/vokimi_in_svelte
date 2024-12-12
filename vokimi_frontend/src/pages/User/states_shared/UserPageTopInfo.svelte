<script lang="ts">
    import LoadingMessage from "../../../components/shared/LoadingMessage.svelte";
    import { Err } from "../../../ts/Err";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { UserPageTopInfoData } from "../../../ts/page_classes/user_page_classes/UserPageTopInfoData";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";
    import AccountBaseStats from "./AccountBaseStats.svelte";

    export let userId: string = "";
    export let openUserAdditionalInfoDialog: () => Promise<void>;

    let pageInfo: UserPageTopInfoData;
    async function fetchData(): Promise<Err> {
        const url = "/api/userPage/getUserPageTopInfoData/" + userId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            pageInfo = UserPageTopInfoData.fromResponseData(data);
            return Err.none();
        } else if (response.status === 400) {
            return new Err(await getErrorFromResponse(response));
        } else {
            return new Err("Something went wrong...");
        }
    }
</script>

{#await fetchData()}
    <LoadingMessage />
{:then fetchingErr}
    {#if fetchingErr.notNone()}
        <div class="err-message">{fetchingErr.toString()}</div>
    {:else}
        <div class="user-info" style="--banner-color: {pageInfo.bannerColor};">
            <div class="banner"></div>
            <div class="info">
                <div class="profile-picture">
                    <img
                        src={ImgUtils.imgUrl(pageInfo.profilePicturePath)}
                        alt="Profile Picture"
                    />
                </div>
                <div class="username-zone">
                    <p class="username">{pageInfo.username}</p>
                    <label
                        class="additional-info-label"
                        on:click={openUserAdditionalInfoDialog}
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="none"
                        >
                            <path
                                d="M22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22C17.5228 22 22 17.5228 22 12Z"
                                stroke="currentColor"
                                stroke-width="1.5"
                            />
                            <path
                                d="M12.2422 17V12C12.2422 11.5286 12.2422 11.2929 12.0957 11.1464C11.9493 11 11.7136 11 11.2422 11"
                                stroke="currentColor"
                                stroke-width="1.5"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                            <path
                                d="M11.992 8H12.001"
                                stroke="currentColor"
                                stroke-width="2"
                                stroke-linecap="round"
                                stroke-linejoin="round"
                            />
                        </svg>
                        Additional info
                    </label>
                </div>
                <slot name="right-side-slot" />
            </div>
        </div>
        <AccountBaseStats
            {userId}
            publishedTestsCount={pageInfo.publishedTestsCount}
            followingsCount={pageInfo.followingsCount}
            friendsCount={pageInfo.friendsCount}
            followersCount={pageInfo.followersCount}
        />
    {/if}
{/await}

<style>
    .user-info {
        width: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .banner {
        width: 100%;
        height: 200px;
        position: relative;
        border-radius: 16px 16px 4px 4px;
        background-color: var(--banner-color);
    }

    .info {
        display: grid;
        grid-template-columns: 140px 1fr auto;
        align-items: center;
        width: 100%;
        padding: 0 28px;
        box-sizing: border-box;
        justify-content: center;
    }

    .profile-picture {
        z-index: 4;
        margin-top: -50%;
        width: 100%;
        aspect-ratio: 1 / 1;
        border-radius: 50%;
        overflow: hidden;
        border: 3px solid var(--banner-color);
        background-color: var(--back-main);
        align-content: center;
    }

    .profile-picture img {
        width: 100%;
        height: 100%;
        object-fit: cover;
    }

    .username-zone {
        margin-left: 28px;
    }

    .username {
        font-size: 32px;
        font-weight: 700;
        margin: 0;
        word-break: break-all;
    }

    .additional-info-label {
        margin-top: 4px;
        display: inline-flex;
        flex-direction: row;
        align-items: center;
        gap: 4px;
        color: var(--text-faded);
        cursor: pointer;
    }

    .additional-info-label svg {
        height: 20px;
    }

    .additional-info-label:hover {
        text-decoration: underline;
        color: var(--primary);
    }

    .additional-info-label:active {
        color: var(--primary-hov);
    }
</style>
