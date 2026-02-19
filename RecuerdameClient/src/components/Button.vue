<script setup lang="ts">
defineProps({
    label: {
        type: String,
        required: true,
    },
    icon: {
        type: String,
        required: true,
    },
    class: {
        type: String,
        required: false,
    },
})
</script>
<template>
    <button :class="class">
        <i :class="icon"></i>
        {{ label }}
    </button>
</template>