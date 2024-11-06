<script lang="ts">
    export let rating: number;
    export let updateRating: () => void;
    export let testId: string;

    let unsavedRating: number = rating;
    function ratingChanged() {}
    async function saveRating() {}
</script>

<div class="ratings-tab-stars-input">
    <label class="property-label">Rate this test:</label>
    ...
    {#if unsavedRating !== rating}
        <button class="save-rating-btn" on:click={saveRating}> Update </button>
    {/if}
</div>
