<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import type { TestCreatorBasicData } from "../../../ts/page_classes/view_test_page_classes/TestCreatorBasicData";
    import { ImgUtils } from "../../../ts/utils/ImgUtils";

    export let creatorData: TestCreatorBasicData;
    export let conditionMessage: string;
    async function follow() {
        const response = await fetch(
            `/api/userPage/followUser/${creatorData.testCreatorId}`,
        );
        if (response.ok) {
            location.reload();
        } else if (response.status === 400) {
            errMessage = await getErrorFromResponse(response);
        } else {
            errMessage = "Unknown error";
        }
    }
    let errMessage: string = "";
</script>

<div class="relationship-conditions-not-met">
    <p class="condition-message">{conditionMessage}</p>
    <img
        src={ImgUtils.imgUrl(creatorData.testCreatorProfilePicture)}
        alt="Profile Pic"
        class="profile-pic"
    />
    <span class="username-span">{creatorData.testCreatorUserName}</span>
    <button class="follow-btn" on:click={follow}>Follow</button>
</div>

<style>
    .profile-pic {
        height: 56px;
        object-fit: contain;
    }
    .username-span {
        font-size: 24px;
    }
    .condition-message {
        text-align: center;
        font-size: 28px;
    }
    .follow-btn {
        color: var(--back-main);
        background-color: var(--primary);
        padding: 6px 16px;
        font-size: 20px;
        border-radius: 4px;
        outline: none;
        border: none;
        cursor: pointer;
    }
    .follow-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
