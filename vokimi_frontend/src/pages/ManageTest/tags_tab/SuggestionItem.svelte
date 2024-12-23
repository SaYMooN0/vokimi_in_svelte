<script lang="ts">
    import { getErrorFromResponse } from "../../../ts/ErrorResponse";
    import type { TagSuggestionForTest } from "../../../ts/page_classes/manage_test_page/tags/TagSuggestionForTest";
    import { StringUtils } from "../../../ts/utils/StringUtils";

    export let suggestion: TagSuggestionForTest;
    export let addThisTagToTest: (newTag: string) => void;
    export let removeThisTagFromSuggestion: () => void;
    export let testId: string;
    let actionErrMsg: string = "";
    async function acceptSuggestion() {
        actionErrMsg = "";
        const response = await fetch(`/api/manageTest/tags/acceptSuggestedTag`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                testId: testId,
                tagSuggestionId: suggestion.id,
            }),
        });
        if (response.ok) {
            const data = await response.json();
            addThisTagToTest(data.acceptedTagValue);
        } else if (response.status === 400) {
            actionErrMsg = await getErrorFromResponse(response);
        } else {
            actionErrMsg = "Something went wrong...";
        }
    }
    async function declineSuggestion() {
        actionErrMsg = "";
        const response = await fetch(
            `/api/manageTest/tags/declineSuggestedTag`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    testId: testId,
                    tagSuggestionId: suggestion.id,
                }),
            },
        );
        if (response.ok) {
            removeThisTagFromSuggestion();
        } else if (response.status === 400) {
            actionErrMsg = await getErrorFromResponse(response);
        } else {
            actionErrMsg = "Something went wrong...";
        }
    }
    async function banSuggestion() {
        actionErrMsg = "";
        const response = await fetch(`/api/manageTest/tags/banSuggestedTag`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                testId: testId,
                tagSuggestionId: suggestion.id,
            }),
        });
    }
</script>

<div class="suggestion">
    <div class="suggestion-info">
        <p class="suggestion-p">
            Tag
            <span class="suggestion-value">
                {suggestion.value}
            </span>
            was suggested
            <span class="suggestion-count">
                {suggestion.suggestionsCount}
            </span>
            times
        </p>
        <span class="suggestion-date">
            First suggestion: {suggestion.firstSuggestionDate.toLocaleDateString()}
        </span>
        {#if !StringUtils.isNullOrWhiteSpace(actionErrMsg)}
            <p class="err-msg">{actionErrMsg}</p>
        {/if}
    </div>
    <div class="suggestion-actions">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            class="accept-btn"
            on:click={acceptSuggestion}
            fill="none"
        >
            <path
                d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
        <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            class="decline-btn"
            on:click={declineSuggestion}
            fill="none"
        >
            <path
                d="M5.25 5L19.25 19"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M22.25 12C22.25 6.47715 17.7728 2 12.25 2C6.72715 2 2.25 6.47715 2.25 12C2.25 17.5228 6.72715 22 12.25 22C17.7728 22 22.25 17.5228 22.25 12Z"
                stroke="currentColor"
                stroke-width="1.8"
            />
        </svg>
        <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            class="ban-btn"
            on:click={banSuggestion}
            fill="none"
        >
            <path
                d="M12 14.0059L5.84373 21.2328C5.01764 22.2026 3.54001 22.2616 2.63922 21.3608C1.73843 20.46 1.79744 18.9824 2.7672 18.1563L9.99412 12"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linejoin="round"
            />
            <path
                d="M22 11.9048L15.9048 18M12.0952 2L6 8.09524M11.3334 2.76186L6.76195 7.33329C6.76195 7.33329 9.04766 10.3809 11.3334 12.6666C13.6191 14.9523 16.6667 17.2381 16.6667 17.2381L21.2381 12.6666C21.2381 12.6666 18.9524 9.61901 16.6667 7.33329C14.381 5.04758 11.3334 2.76186 11.3334 2.76186Z"
                stroke="currentColor"
                stroke-width="1.8"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </div>
</div>

<style>
    .suggestion {
        display: grid;
        grid-template-columns: 1fr auto;
        padding: 4px 8px;
        border-radius: 8px;
    }
    .suggestion:hover {
        background-color: var(--back-secondary);
    }
    .suggestion-info {
        display: flex;
        flex-direction: column;
    }
    .suggestion-p {
        font-size: 18px;
        margin: 2px 0;
    }
    .suggestion-p span {
        color: var(--primary);
        font-weight: 500;
    }
    .suggestion-date {
        color: var(--text-faded);
        font-size: 16px;
    }
    .suggestion-actions {
        display: flex;
        flex-direction: row;
        gap: 4px;
    }
    .suggestion-actions svg {
        height: 28px;
        width: 28px;
        padding: 4px;
        box-sizing: border-box;
        border-radius: 4px;
        color: var(--back-main);
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .suggestion-actions svg:hover {
        padding: 3px;
    }
    .suggestion-actions svg:active {
        transform: scale(0.96);
    }
    .accept-btn {
        background-color: var(--primary);
    }
    .decline-btn {
        background-color: var(--text-faded);
    }
    .ban-btn {
        background-color: var(--red-del);
    }
</style>
