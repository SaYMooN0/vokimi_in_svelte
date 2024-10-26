<script lang="ts">
    import BaseDialog from "../../../components/BaseDialog.svelte";
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import { UserAdditionalInfo } from "../../../ts/page_classes/user_page_classes/UserAdditionalInfo";
    import { StringUtils } from "../../../ts/utils/StringUtils";
    import FacebookIcon from "../social_icons/FacebookIcon.svelte";
    import OtherLinkIcon from "../social_icons/OtherLinkIcon.svelte";
    import TelegramIcon from "../social_icons/TelegramIcon.svelte";
    import XIcon from "../social_icons/XIcon.svelte";
    import YouTubeIcon from "../social_icons/YouTubeIcon.svelte";

    export let userId: string;
    let userInfo: UserAdditionalInfo = UserAdditionalInfo.empty();
    let fetchingErr: string = "Unable to fetch data";
    let dialogElement: BaseDialog;
    export async function open() {
        fetchingErr = "";
        const url = "/api/userPage/getUserPageAdditionalInfo/" + userId;
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            console.log(data);
            userInfo = new UserAdditionalInfo(
                data.realName,
                data.registrationDate,
                data.birthDate,
                data.links,
                data.linksMassage,
            );
        } else if (response.status === 400) {
            fetchingErr = await getErrorFromResponse(response);
        } else {
            fetchingErr = "Unknown error";
        }
        dialogElement.open();
    }
    function getLinkIcon(linkKey: string) {
        const linksKeyValues: Record<string, any> = {
            Telegram: TelegramIcon,
            YouTube: YouTubeIcon,
            Facebook: FacebookIcon,
            X: XIcon,
            Other1: OtherLinkIcon,
            Other2: OtherLinkIcon,
        };
        return linksKeyValues[linkKey];
    }
</script>

<BaseDialog dialogId="viewUserAdditionalInfoDialog" bind:this={dialogElement}>
    <div class="dialog-body">
        <svg
            class="close-btn"
            on:click={() => dialogElement.close()}
            xmlns="http://www.w3.org/2000/svg"
            xmlns:xlink="http://www.w3.org/1999/xlink"
            viewBox="0 0 384 512"
        >
            <path
                d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"
            />
        </svg>

        <p class="dialog-header">Additional info:</p>
        <div class="prop-val-div">
            <span class="info-prop">Real Name:</span>
            <span class="info-val">{userInfo.realName}</span>
            <span class="info-prop">Birth Date:</span>
            <span class="info-val">{userInfo.birthDate}</span>
            <span class="info-prop">Registration Date:</span>
            <span class="info-val">{userInfo.registrationDate}</span>
        </div>
        {#if Object.entries(userInfo.links).length > 0}
            <p class="dialog-header">User links:</p>
            <div class="user-links">
                {#each Object.entries(userInfo.links) as linkKVP}
                    <div class="link-div">
                        <div class="link-icon">
                            <svelte:component this={getLinkIcon(linkKVP[0])} />
                        </div>
                        {#if linkKVP[1].startsWith("http://") || linkKVP[1].startsWith("https://")}
                            <a
                                href={linkKVP[1]}
                                target="_blank"
                                class="link-val"
                            >
                                {linkKVP[1]}
                            </a>
                        {:else}
                            <label class="link-no-val">{linkKVP[1]}</label>
                        {/if}
                    </div>
                {/each}
            </div>
        {:else if StringUtils.isNullOrWhiteSpace(userInfo.linksMassage)}
            <label class="no-links-message">User has not set any links</label>
        {:else}
            <label class="no-links-message">{userInfo.linksMassage}</label>
        {/if}
        <slot></slot>
    </div>
</BaseDialog>

<style>
    .dialog-body {
        position: relative;
        padding: 12px 20px 8px 20px;
        width: 600px;
    }
    .close-btn {
        position: absolute;
        right: 12px;
        top: 12px;
        background-color: var(--text-faded);
        height: 30px;
        padding: 5px;
        box-sizing: border-box;
        aspect-ratio: 1/1;
        border-radius: 50%;
        fill: var(--back-main);
        cursor: pointer;
        transition: all 0.14 ease-in;
    }
    .close-btn:hover {
        transform: scale(1.08);
    }
    .dialog-header {
        margin: 8px 0px;
        font-size: 24px;
        font-weight: 600;
    }
    .prop-val-div {
        padding-left: 8px;
        display: grid;
        grid-template-columns: auto 1fr;
        grid-template-rows: auto;
        column-gap: 20px;
        row-gap: 4px;
    }
    .info-prop {
        font-size: 18px;
    }
    .info-val {
        font-size: 20px;
        justify-self: end;
    }
    .user-links {
        display: flex;
        flex-direction: column;
        gap: 8px;
    }
    .link-div {
        display: flex;
        flex-direction: row;
        align-items: center;
        height: 28px;
    }
    .link-icon {
        height: 100%;
    }
    .link-icon :global(svg) {
        height: 100%;
        margin-right: 8px;
        color: var(--primary);
    }
    .link-val {
        font-size: 20px;
        color: var(--primary);
    }
    .link-val:hover {
        text-decoration: underline;
    }
    .link-val:active {
        color: var(--primary-hov);
    }
    .link-no-val {
        font-size: 20px;
        color: var(--text-faded);
    }
    .no-links-message {
        margin: 12px 0;
        width: 100%;
        display: flex;
        justify-content: center;
        font-size: 18px;
        color: var(--text-faded);
    }
</style>
