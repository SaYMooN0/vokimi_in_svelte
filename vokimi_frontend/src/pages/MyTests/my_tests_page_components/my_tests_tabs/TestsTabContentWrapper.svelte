<script lang="ts">
    import type { Err } from "../../../../ts/Err";

    export let fetchTestsFunc: (skipLengthCheck: boolean) => Promise<Err>;
    export let yourTestsLabel: string = "";
</script>

{#await fetchTestsFunc(false)}
    <div class="loading-div">
        <h2>Receiving information from server...</h2>
        <p>Please wait</p>
    </div>
{:then err}
    {#if err.notNone()}
        <div class="error-div">
            <p class="error">{err.toString()}</p>
        </div>
    {:else}
        <h2 class="your-draft-tests-label">{yourTestsLabel}</h2>
        <div class="refresh-btn" on:click={() => fetchTestsFunc(true)}>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
            >
                <path
                    d="M15.1667 0.999756L15.7646 2.11753C16.1689 2.87322 16.371 3.25107 16.2374 3.41289C16.1037 3.57471 15.6635 3.44402 14.7831 3.18264C13.9029 2.92131 12.9684 2.78071 12 2.78071C6.75329 2.78071 2.5 6.90822 2.5 11.9998C2.5 13.6789 2.96262 15.2533 3.77093 16.6093M8.83333 22.9998L8.23536 21.882C7.83108 21.1263 7.62894 20.7484 7.7626 20.5866C7.89627 20.4248 8.33649 20.5555 9.21689 20.8169C10.0971 21.0782 11.0316 21.2188 12 21.2188C17.2467 21.2188 21.5 17.0913 21.5 11.9998C21.5 10.3206 21.0374 8.74623 20.2291 7.39023"
                    stroke="currentColor"
                    stroke-width="1.5"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                />
            </svg>
            Refresh
        </div>
        <div class="slot-frame">
            <slot />
        </div>
    {/if}
{/await}

<style>
    .your-draft-tests-label {
        margin: 20px auto 0 auto;
        width: fit-content;
        font-size: 26px;
    }
    .refresh-btn {
        margin: 0 auto 20px auto;
        color: var(--text-faded);
        font-size: 16px;
        width: fit-content;
        padding: 4px 8px;
        border-radius: 8px;
        display: flex;
        align-items: center;
        gap: 4px;
        cursor: pointer;
        transition: all 0.08s;
    }
    .refresh-btn svg {
        height: 20px;
    }
    .refresh-btn:hover {
        color: var(--primary);
        background-color: var(--back-secondary);
    }
    .refresh-btn:active {
        color: var(--primary-hov);
        transform: scale(0.98);
    }
    .slot-frame {
        width: 1320px;
        margin: 0 auto;
    }
</style>
