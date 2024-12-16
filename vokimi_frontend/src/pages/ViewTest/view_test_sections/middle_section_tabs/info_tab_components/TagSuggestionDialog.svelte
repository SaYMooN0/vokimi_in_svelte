<script lang="ts">
    import AuthorizeView from "../../../../../components/AuthorizeView.svelte";
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import TagOperatingDisplay from "../../../../../components/shared/tags/TagOperatingDisplay.svelte";

    let dialogElement: BaseDialog;
    let popularSuggestions: string[] = [];
    let chosenTags: string[] = [];
    export let testId: string;
    export async function open() {
        dialogElement.open();
        if (popularSuggestions.length === 0) {
            const response = await fetch(
                "/api/tags/getMostSuggestedTags/" + testId,
            );
            if (response.ok) {
            }
        }
    }
    function removeTag(tag: string) {
        chosenTags = chosenTags.filter((t) => t !== tag);
    }
    function addTag(tag: string) {
        if (!chosenTags.includes(tag)) {
            chosenTags = [...chosenTags, tag];
        }
    }
</script>

<BaseDialog dialogId="tagSuggestionDialog" bind:this={dialogElement}>
    <div class="dialog-content">
        <CloseButton onClose={() => dialogElement.close()} />
        <AuthorizeView>
            <svelte:fragment slot="unauthenticated">
                <p>You have to be logged in to suggest tags</p>
                <div>
                    <a href="/auth/login">Login</a>
                    <a href="/auth/signup">Create an account</a>
                </div>
            </svelte:fragment>
            <svelte:fragment slot="authenticated">
                <div class="dialog-left-side">
                    {#if popularSuggestions.length > 0}
                        <p class="dialog-p">
                            Choose most suggested tags to support their adding
                            to the test
                        </p>
                        <div class="tags-container">
                            {#each popularSuggestions as tag}
                                <TagOperatingDisplay
                                    {tag}
                                    isTagAdded={chosenTags.includes(tag)}
                                    isTagAddingState={false}
                                    btnOnClick={() => addTag(tag)}
                                />
                            {/each}
                        </div>
                        <p class="dialog-p-or">or</p>
                    {/if}
                    <p class="dialog-p">Search for tags you want to suggest</p>
                    <input class="search-bar" placeholder="Input tag" />
                </div>
                <div class="dialog-right-side">
                    <p>List of tags to suggest</p>
                    <div class="suggested-tags-container"></div>
                </div>
            </svelte:fragment>
        </AuthorizeView>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: grid;
        grid-template-columns: 1fr 1fr;
        position: relative;
    }
</style>
