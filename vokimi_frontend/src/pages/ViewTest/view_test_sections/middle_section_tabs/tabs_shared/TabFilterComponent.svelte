<script lang="ts">
    import AuthorizeView from "../../../../../components/AuthorizeView.svelte";
    import { Err } from "../../../../../ts/Err";
    import { StringUtils } from "../../../../../ts/utils/StringUtils";

    export let clearFilter: () => void;
    export let applyFilter: () => Promise<void | Err>;

    async function applyFilterBtnPressed() {
        filterApplyingError = "";
        const res = await applyFilter();
        if (res instanceof Err) {
            filterApplyingError = res.toString();
        }
    }
    function toggleButtonClicked() {
        isHidden = !isHidden;
    }
    let isHidden: boolean = true;
    let filterApplyingError: string = "";
</script>

<div on:click={toggleButtonClicked} class="show-filter-btn unselectable">
    <svg
        class="filter-icon"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
    >
        <path
            d="M8.85746 12.5061C6.36901 10.6456 4.59564 8.59915 3.62734 7.44867C3.3276 7.09253 3.22938 6.8319 3.17033 6.3728C2.96811 4.8008 2.86701 4.0148 3.32795 3.5074C3.7889 3 4.60404 3 6.23433 3H17.7657C19.396 3 20.2111 3 20.672 3.5074C21.133 4.0148 21.0319 4.8008 20.8297 6.37281C20.7706 6.83191 20.6724 7.09254 20.3726 7.44867C19.403 8.60062 17.6261 10.6507 15.1326 12.5135C14.907 12.6821 14.7583 12.9567 14.7307 13.2614C14.4837 15.992 14.2559 17.4876 14.1141 18.2442C13.8853 19.4657 12.1532 20.2006 11.226 20.8563C10.6741 21.2466 10.0043 20.782 9.93278 20.1778C9.79643 19.0261 9.53961 16.6864 9.25927 13.2614C9.23409 12.9539 9.08486 12.6761 8.85746 12.5061Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    {#if isHidden}
        Show filter
    {:else}
        Hide filter
    {/if}
</div>

<form
    class="filter-base"
    class:is-hidden={isHidden}
    on:submit|preventDefault={applyFilterBtnPressed}
>
    <slot name="filter-content"></slot>

    <AuthorizeView>
        <div slot="authenticated">
            <slot name="authenticated-filters"></slot>
        </div>
        <div slot="unauthenticated" class="login-message unselectable">
            Please log in to access all filter options
        </div>
    </AuthorizeView>

    {#if !StringUtils.isNullOrWhiteSpace(filterApplyingError)}
        <p class="error-message">{filterApplyingError}</p>
    {/if}

    <div class="form-buttons">
        <button type="button" on:click={() => clearFilter()}>Clear</button>
        <button type="submit">Apply</button>
    </div>
</form>

<style>
    .show-filter-btn {
        margin: 12px 8px 12px auto;
        display: grid;
        grid-template-columns: auto 1fr;
        align-items: center;
        cursor: pointer;
        width: 120px;
        padding: 4px 16px;
        gap: 4px;
        border-radius: 20px;
        font-size: 18px;
        background-color: var(--back-secondary);
        border: 2px solid var(--back-secondary);
    }
    .show-filter-btn:hover {
        border-color: var(--primary);
    }
    .show-filter-btn:active {
        transform: scale(0.98);
    }
    .show-filter-btn .filter-icon {
        color: inherit;
        width: 24px;
        height: 24px;
    }
    .filter-base {
        display: block;
        background-color: var(--back-secondary);
        padding: 4px 16px;
        border-radius: 8px;
        box-sizing: border-box;
        margin-bottom: 12px;
    }
    .is-hidden {
        display: none;
    }
    .login-message {
        color: var(--text-faded);
        font-size: 16px;
        margin: 4px auto;
        width: fit-content;
    }
    .form-buttons {
        display: flex;
        gap: 8px;
        justify-content: flex-end;
    }
</style>
