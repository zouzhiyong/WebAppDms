<template>
  <div>
    <slot name="newButton" v-if='contains($route.meta.button,"new") || $route.meta.isButton==false' style="float:right;margin-right:40px;"></slot>
    <slot name="cancleButton" v-if='contains($route.meta.button,"cancle") || $route.meta.isButton==true'></slot>
    <slot name="saveButton" v-if='contains($route.meta.button,"save") || $route.meta.isButton==true'></slot>
  </div>
</template>
<script>
  export default {
    methods: {
      contains(arr, obj) {
        var i = arr.length;
        while (i--) {
          if (arr[i] === obj) {
            return true;
          }
        }
        return false;
      }
    }
  };
</script>