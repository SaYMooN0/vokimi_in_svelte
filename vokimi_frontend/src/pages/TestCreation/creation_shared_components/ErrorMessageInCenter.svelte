<script lang="ts">
    export let errorMessage: string = "An unknown error occurred.";
</script>

<div class="error-message-container">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
        <path
            d="M5.32171 9.6829C7.73539 5.41196 8.94222 3.27648 10.5983 2.72678C11.5093 2.42437 12.4907 2.42437 13.4017 2.72678C15.0578 3.27648 16.2646 5.41196 18.6783 9.6829C21.092 13.9538 22.2988 16.0893 21.9368 17.8293C21.7376 18.7866 21.2469 19.6548 20.535 20.3097C19.241 21.5 16.8274 21.5 12 21.5C7.17265 21.5 4.75897 21.5 3.46496 20.3097C2.75308 19.6548 2.26239 18.7866 2.06322 17.8293C1.70119 16.0893 2.90803 13.9538 5.32171 9.6829Z"
            stroke="currentColor"
            stroke-width="1.5"
        />
        <path
            d="M11.992 16H12.001"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
        <path
            d="M12 13L12 8.99997"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
        />
    </svg>
    <p class="error-message">{errorMessage}</p>
</div>

<style>
    .error-message-container {
        position: absolute;
        top: 36vh;
        left: 50%;
        transform: translateX(-50%);
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--back-secondary);
        padding: 10px 20px;
        border-radius: 8px;
    }
    .error-message-container svg {
        color: #df2950;
        height: 60px;
    }
    .error-message {
        margin-top: 20px;
        text-align: center;
        font-size: 24px;
        max-width: 600px;
    }
</style>
