<script lang="ts">
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";

    export let rating: number;
    export let updateRating: (rating: number) => void;
    export let testId: string;

    let unsavedRating: number = rating;
    let ratingSavingErr: string = "";
    async function saveRating() {
        ratingSavingErr = "";
        const data = { rating: unsavedRating, testId };
        const response = await fetch("/api/viewTest/ratings/updateTestRating", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            rating = unsavedRating;
            updateRating(unsavedRating);
        } else if (response.status === 400) {
            ratingSavingErr = await getErrorFromResponse(response);
        } else {
            ratingSavingErr = "An unknown error occurred.";
        }
    }
</script>

<div class="ratings-tab-stars-input">
    <label class="rate-test-label">Rate this test:</label>
    <div class="rating">
        {#each [5, 4, 3, 2, 1] as star}
            <input
                type="radio"
                id="star-{star}"
                name="star-radio"
                value={star}
                bind:group={unsavedRating}
            />
            <label for="star-{star}">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                    <path
                        pathLength="360"
                        d="M12,17.27L18.18,21L16.54,13.97L22,9.24L14.81,8.62L12,2L9.19,8.62L2,9.24L7.45,13.97L5.82,21L12,17.27Z"
                    />
                </svg>
            </label>
        {/each}
    </div>
    {#if unsavedRating !== rating}
        <button class="save-rating-btn" on:click={saveRating}>Update</button>
    {/if}
</div>
{#if !StringUtils.isNullOrWhiteSpace(ratingSavingErr)}
    <p class="rating-saving-err">
        {ratingSavingErr}
    </p>
{/if}

<style>
    .ratings-tab-stars-input {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 12px;
    }
    .rate-test-label {
        font-size: 18px;
        font-weight: 500;
    }
    .rating {
        display: flex;
        flex-direction: row-reverse;
        gap: 0;
        --stroke: var(--text-faded);
        --fill: #ffc73a;
    }

    .rating input {
        appearance: unset;
    }

    .rating label {
        cursor: pointer;
    }

    .rating svg {
        width: 2rem;
        height: 2rem;
        overflow: visible;
        fill: transparent;
        stroke: var(--stroke);
        stroke-linejoin: round;
        stroke-dasharray: 12;
        transition:
            stroke 0.2s,
            fill 0.5s;
    }

    .rating label:hover svg {
        stroke: var(--fill);
    }

    .rating input:checked ~ label svg {
        transition: 0s;
        animation: yippee 0.65s backwards;
        fill: var(--fill);
        stroke: var(--fill);
        stroke-opacity: 0;
        stroke-dasharray: 0;
        stroke-linejoin: miter;
        stroke-width: 8px;
    }
    .rating-saving-err {
        color: var(--red-del);
        font-size: 16px;
    }
    .save-rating-btn {
        padding: 6px 14px;
        background-color: var(--primary);
        color: var(--back-main);
        font-size: 16px;
        border-radius: 4px;
        outline: none;
        border: none;
        cursor: pointer;
    }
    .save-rating-btn:hover {
        background-color: var(--primary-hov);
    }
    .save-rating-btn:active {
        transform: scale(0.98);
    }
    @keyframes yippee {
        0% {
            transform: scale(1);
            fill: var(--fill);
            fill-opacity: 0;
            stroke-opacity: 1;
            stroke: var(--stroke);
            stroke-dasharray: 10;
            stroke-width: 1px;
            stroke-linejoin: bevel;
        }

        30% {
            transform: scale(0.6);
            fill: var(--fill);
            fill-opacity: 0;
            stroke-opacity: 1;
            stroke: var(--stroke);
            stroke-dasharray: 10;
            stroke-width: 1px;
            stroke-linejoin: bevel;
        }

        30.1% {
            stroke: var(--fill);
            stroke-dasharray: 0;
            stroke-linejoin: miter;
            stroke-width: 8px;
        }

        60% {
            transform: scale(1.02);
            fill: var(--fill);
        }
    }
</style>
