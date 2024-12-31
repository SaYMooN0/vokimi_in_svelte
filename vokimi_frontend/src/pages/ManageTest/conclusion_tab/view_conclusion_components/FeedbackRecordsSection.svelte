<script lang="ts">
    import { getErrorFromResponse } from "../../../../ts/ErrorResponse";
    import { FeedbackRecordData } from "../../../../ts/page_classes/manage_test_page/conclusion/FeedbackRecordData";
    import type { FeedbackRecordsFilter } from "../../../../ts/page_classes/manage_test_page/conclusion/FeedbackRecordsFilter";
    import { ImgUtils } from "../../../../ts/utils/ImgUtils";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import FeedbackRecordsFilterComponent from "./feedback_records_section.svelte/FeedbackRecordsFilterComponent.svelte";
    export let testId: string;

    let records: FeedbackRecordData[] = [];
    let fetchError: string;
    async function fetchRecords() {
        const response = await fetch(
            "/api/manageTest/conclusion/feedbackRecords/" + testId,
        );
        if (response.ok) {
            const data = await response.json();
            records = data.map((record: any) =>
                FeedbackRecordData.fromResponseData(record),
            );
        } else if (response.status === 400) {
            fetchError = await getErrorFromResponse(response);
        } else {
            fetchError = "Unable to fetch feedback records";
        }
    }
    async function fetchFilteredRecords(filter: FeedbackRecordsFilter) {
        const bodyFilter = {
            ...filter,
            dateFrom: filter.dateFrom === "" ? null : filter.dateFrom,
            dateTo: filter.dateTo === "" ? null : filter.dateTo,
        };
        const response = await fetch(
            "/api/manageTest/conclusion/filteredFeedbackRecords/" + testId,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(bodyFilter),
            },
        );
        if (response.ok) {
            const data = await response.json();
            records = data.map((record: any) =>
                FeedbackRecordData.fromResponseData(record),
            );
        } else if (response.status === 400) {
            fetchError = await getErrorFromResponse(response);
        } else {
            fetchError = "Unable to fetch filtered feedback records";
        }
    }
</script>

<div class="feedback-records-section">
    <p class="section-subheader">Users' feedback</p>

    <FeedbackRecordsFilterComponent
        fetchFilteredRecords={(filter) => fetchFilteredRecords(filter)}
        fetchRecords={() => fetchRecords()}
    />
    {#await fetchRecords() then _}
        {#if !StringUtils.isNullOrWhiteSpace(fetchError)}
            <p class="err-msg">{fetchError}</p>
        {:else if records.length == 0}
            <p class="no-records-p">No feedback found</p>
        {:else}
            {#each records as record}
                <div class="feedback-record">
                    <p class="username-with-pic">
                        <img
                            src={ImgUtils.imgUrl(record.authorProfilePicture)}
                            alt="profile pic"
                            class="profile-pic"
                        />
                        {#if record.isAnonymous()}
                            <span class="username">
                                {record.authorUsername}
                            </span>
                        {:else}
                            <a
                                href="/user/{record.userId}"
                                class="username user-link"
                            >
                                {record.authorUsername}
                            </a>
                        {/if}
                    </p>
                    <span class="feedback">{record.text}</span>
                    <span class="date">{record.createdAt.toLocaleString()}</span
                    >
                </div>
            {/each}
        {/if}
    {/await}
</div>

<style>
    .no-records-p {
        font-size: 18px;
        color: var(--text-faded);
    }
    .feedback-record {
        display: flex;
        flex-direction: column;
        padding: 2px 12px;
        margin-bottom: 12px;
        border-radius: 8px;
        box-shadow: 0 1px 4px rgba(0, 0, 0, 0.08);
    }
    .username-with-pic {
        width: 100%;
        display: grid;
        grid-template-columns: auto 1fr;
        gap: 4px;
        align-items: center;
        margin: 2px 0;
        box-sizing: border-box;
    }

    .profile-pic {
        width: 48px;
        height: 48px;
        border-radius: 50%;
    }

    .username {
        width: fit-content;
        font-size: 20px;
        color: var(--primary);
        padding: 2px 4px;
        box-sizing: border-box;
        border-radius: 4px;
    }
    .user-link {
        text-decoration: underline;
    }
    .user-link:hover {
        background-color: var(--back-secondary);
    }
    .date {
        font-size: 16px;
        color: var(--text-faded);
        margin-top: 5px;
        align-self: flex-end;
    }
</style>
