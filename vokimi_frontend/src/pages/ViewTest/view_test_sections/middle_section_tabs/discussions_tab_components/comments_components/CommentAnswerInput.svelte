<script lang="ts">
    export let parentCommentId: string;
    let isHidden = true;
    export function show() {
        isHidden = false;
    }
</script>

<div class="answer-input-container" class:hidden={isHidden}>
    <svg
        class="close-btn-svg"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
        fill="none"
        on:click={() => {
            isHidden = true;
        }}
    >
        <path
            d="M19.0005 4.99988L5.00049 18.9999M5.00049 4.99988L19.0005 18.9999"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    <input />
</div>

<style>

    .answer-input-container {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 1rem;
    }
    .hidden {
        display: none;
    }
    .close-btn-svg {
        cursor: pointer;
        height: 20px;
        aspect-ratio: 1/1;
        background-color: var(--text-faded);
        color: var(--back-primary);
    }
    .close-btn-svg:hover {
        background-color: var(--red-del);
    }
</style>
