<script script lang="ts">
    import AuthorizeView from "../../../../components/AuthorizeView.svelte";
    import { Err } from "../../../../ts/Err";

    export let testId: string = "";
    async function fetchRatingsListPackage() {}
    async function fetchViewerRating(): Promise<number | Err> {
        return new Err("not implemented");
    }
</script>

<p class="average-rating">Test average rating: ...</p>
<div class="viewer-rating">
    <AuthorizeView>
        <div slot="loading">
            <p>Loading...</p>
        </div>
        <p slot="unauthenticated">
            you have to log in to rate the test if has rated
        </p>
        <div slot="authenticated" class="viewer-rating">
            {#await fetchViewerRating()}
                <p>Loading...</p>
            {:then fetchRes}
                {#if fetchRes instanceof Err}
                    <p>{fetchRes.toString()}</p>
                {:else}
                    <p>{fetchRes}</p>
                {/if}
            {/await}
        </div>
    </AuthorizeView>
    if not authenticated ... you have to log in to rate the test if has rated if
    has rated ... you rate this test: if hasn't rated ... rate this test:
</div>
<div class="ratings-list"></div>
