<script lang="ts">
    import AuthorizeView from "../../../../../components/AuthorizeView.svelte";
    import BaseDialog from "../../../../../components/BaseDialog.svelte";
    import CloseButton from "../../../../../components/shared/CloseButton.svelte";
    import TagOperatingDisplay from "../../../../../components/shared/tags/TagOperatingDisplay.svelte";
    import TagsSearchBar from "../../../../../components/shared/tags/TagsSearchBar.svelte";
    import { getErrorFromResponse } from "../../../../../ts/ErrorResponse";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";

    let dialogElement: BaseDialog;
    let popularSuggestions: string[] = [];
    let chosenTags: string[] = [];
    let searchingErr: string = "";
    let tagsFromSearch: string[] = [];
    let tagsSearchBar: TagsSearchBar;
    let submitErrMsg: string = "";
    export let testId: string;
    export async function open() {
        dialogElement.open();
        if (popularSuggestions.length === 0) {
            const response = await fetch(
                "/api/tags/getMostSuggestedTags/" + testId,
            );
            if (response.ok) {
                popularSuggestions = await response.json();
                console.log("popularSuggestions", popularSuggestions);
            }
        }
        chosenTags = [];
        searchingErr = "";
        tagsFromSearch = [];
        tagsSearchBar.setSearchInputEmpty();
        submitErrMsg = "";
    }
    function removeTag(tag: string) {
        chosenTags = chosenTags.filter((t) => t !== tag);
    }
    function addTag(tag: string) {
        if (!chosenTags.includes(tag)) {
            chosenTags = [...chosenTags, tag];
        }
    }
    export async function saveSuggestedTags() {
        submitErrMsg = "";
        const response = await fetch("/api/tags/suggestTagsForTest/" + testId, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(chosenTags),
        });
        if (response.ok) {
            dialogElement.close();
        } else if (response.status === 400) {
            submitErrMsg = await getErrorFromResponse(response);
        } else {
            submitErrMsg = "Unknown error";
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
                    <TagsSearchBar
                        bind:this={tagsSearchBar}
                        setErrorMessage={(errMsg) => (searchingErr = errMsg)}
                        setSearchedTags={(searchedTags) =>
                            (tagsFromSearch = searchedTags)}
                    />
                    <div class="tags-to-choose-list">
                        {#each tagsFromSearch as tag}
                            <TagOperatingDisplay
                                {tag}
                                btnOnClick={() => addTag(tag)}
                                isTagAdded={chosenTags.includes(tag)}
                                isTagAddingState={true}
                            />
                        {/each}
                    </div>
                    {#if !StringUtils.isNullOrWhiteSpace(searchingErr)}
                        <p class="err-msg">{searchingErr}</p>
                    {/if}
                    <span class="continue-input">
                        If you don't find the tag you need continue entering the
                        name of the tag
                    </span>
                </div>
                <div class="dialog-right-side">
                    <p>List of tags to suggest</p>
                    <div class="suggested-tags-container">
                        {#each chosenTags as tag}
                            <TagOperatingDisplay
                                {tag}
                                isTagAdded={true}
                                isTagAddingState={false}
                                btnOnClick={() => removeTag(tag)}
                            />
                        {/each}
                    </div>
                    {#if !StringUtils.isNullOrWhiteSpace(submitErrMsg)}
                        <p class="err-msg">{submitErrMsg}</p>
                    {/if}
                    <button
                        class="submit-btn"
                        disabled={chosenTags.length < 1}
                        on:click={saveSuggestedTags}
                    >
                        Suggest {chosenTags.length} tags
                    </button>
                </div>
            </svelte:fragment>
        </AuthorizeView>
    </div>
</BaseDialog>

<style>
    .dialog-content {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 12px;
        position: relative;
        padding: 12px 20px;
        min-height: 320px;
    }
    .dialog-content div {
        display: flex;
        flex-direction: column;
        align-items: center;
        max-height: 420px;
        max-width: 620px;
        overflow-y: auto;
    }
    .err-msg {
        color: var(--red-del);
        font-size: 16px;
        font-weight: 500;
    }
    .continue-input {
        margin-top: auto;
        color: var(--text-faded);
        font-size: 16px;
    }
    .submit-btn {
        margin-top: auto;
        background-color: var(--primary);
        color: var(--back-main);
        border: none;
        border-radius: 4px;
        padding: 4px 16px;
        font-size: 18px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }
    .submit-btn:hover {
        background-color: var(--primary-hov);
    }
</style>
