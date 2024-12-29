<script lang="ts">
    import type { FeedbackRecordData } from "../../../../ts/page_classes/manage_test_page/conclusion/FeedbackRecordData";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";

    export let records: FeedbackRecordData[];
</script>

<div class="feedback-records-section">
    <p class="section-subheader">Users' feedback</p>
    {#if records.length == 0}
        <p class="no-records-p">Users have not given any feedback yet</p>
    {:else}
        {#each records as record}
            <div class="feedback-record">
                <img
                    src={ImgUtils.imgUrl(record.authorProfilePicture)}
                    alt="profile pic"
                    class="profile-pic"
                />
                {#if record.isAnonymous()}
                    <span class="username">{record.authorUsername}</span>
                {:else}
                    <a href="/user/{record.authorId}" class="username user-link">
                        {record.authorUsername}
                    </a>
                {/if}
                <span class="feedback">{record.text}</span>
                <span class="date">{record.date.toLocaleDateString()}</span>
            </div>
        {/each}
    {/if}
</div>

<style>
    .no-records-p {
        font-size: 18px;
        color: var(--text-faded);
    }
    .feedback-record {
        display: flex;
        padding: 4px 12px;
        margin-bottom: 12px;
        border-radius: 8px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .profile-pic {
        width: 40px;
        height: 40px;
        border-radius: 50%;
        margin-right: 10px;
    }

    .username {
        font-size: 1rem;
        font-weight: bold;
        color: #007bff;
        margin: 5px 0;
        text-decoration: none;
    }

    .username:hover {
        text-decoration: underline;
    }

    .feedback {
        font-size: 1rem;
        color: #444;
        margin: 10px 0;
    }

    .date {
        font-size: 0.875rem;
        color: #777;
        margin-top: 5px;
        align-self: flex-end;
    }

    .feedback-record {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 10px;
    }
</style>
